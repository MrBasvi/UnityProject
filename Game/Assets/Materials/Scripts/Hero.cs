using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Класс Hero, наследующий Sounds, управляет поведением игрока в игре.
public class Hero : Sounds
{
    // Кнопка паузы.
    public GameObject PauseButton;

    // Панель паузы.
    public GameObject PausePanel;

    // Скорость движения игрока.
    [SerializeField] public float speed = 3f;

    // Количество жизней игрока.
    public int lives;

    // Сила прыжка игрока.
    static public float jumpForce = 15f;

    // Компонент Rigidbody2D для управления физикой игрока.
    public Rigidbody2D rb;

    // Компонент Animator для управления анимациями игрока.
    private Animator anim;

    // Компонент SpriteRenderer для управления спрайтом игрока.
    public SpriteRenderer sprite;

    // Точка проверки нахождения на земле.
    public Transform GroundCheck;

    // Точка проверки нахождения в воде.
    public Transform WaterCheck;

    // Маска слоя для земли.
    public LayerMask WhatIsGround;

    // Маска слоя для зоны смерти.
    public LayerMask WhatIsDeathZone;

    // Маска слоя для мобов.
    public LayerMask WhatIsMobs;

    // Маска слоя для воды.
    public LayerMask WhatIsWater;

    // Радиус проверки нахождения на земле.
    public float RadiusGroundCheck;

    // Радиус проверки нахождения в воде.
    public float RadiusWaterCheck;

    // Флаг, указывающий, находится ли игрок на земле.
    public bool IsGrounded;

    // Флаг, указывающий, находится ли игрок на мобах.
    public bool IsGroundedOnMobs;

    public Transform GroundUpCheck;
    public bool IsGroundUp;
    public float RadiusGroundUpCheck;

    // Флаг, указывающий, находится ли игрок в зоне смерти.
    private bool IsDeathZone;

    // Массив изображений сердец для отображения здоровья.
    [SerializeField] private Image[] hearts;

    // Текущее количество здоровья игрока.
    [SerializeField] static private int health;

    // Спрайт живого сердца.
    [SerializeField] private Sprite alivehearts;

    // Спрайт мертвого сердца.
    [SerializeField] private Sprite deadhearts;

    // Время неуязвимости после получения урона.
    public float timeInvicible = 2f;

    // Флаг, указывающий, является ли игрок неуязвимым.
    public bool isInvicible;

    // Таймер неуязвимости.
    float invicibleTimer;

    // Флаг, указывающий, атакует ли игрок.
    public bool IsAttacking = false;

    // Флаг, указывающий, перезаряжен ли игрок.
    public bool IsRecharged = true;

    // Префаб пули.
    public GameObject bullet;

    // Точка спауна пули справа.
    public Transform BulletTransformRight;

    // Точка спауна пули слева.
    public Transform BulletTransformLeft;

    // Направление движения игрока.
    public string HeroDir = "Right";

    // Флаг, указывающий, бежит ли игрок.
    public bool IsRunning = false;

    // Флаг, указывающий, сидит ли игрок.
    public bool IsSitting = false;

    // Флаг, указывающий, движется ли игрок вправо.
    public bool IsRight = false;

    // Ссылки на компоненты BoxCollider2D.
    public BoxCollider2D boxCollider1;
    public BoxCollider2D boxCollider2;
    public BoxCollider2D boxCollider3;
    public BoxCollider2D boxCollider4;
    public BoxCollider2D boxCollider5;

    // Оригинальный цвет спрайта игрока.
    private Color originalColor;

    // Флаг, указывающий, использует ли игрок костыль.
    public bool Crutch;

    // Статическое свойство для доступа к экземпляру класса.
    public static Hero Instance { get; set; }

    // Флаг, указывающий, находится ли игрок в воде.
    private bool isInWater = false;

    // Множитель скорости в воде.
    public float waterSpeedMultiplier = 0.5f;

    // Время, проведенное в воде.
    private float timeInWater = 0f;

    // Максимальное время в воде без урона.
    private float maxTimeInWater = 10f;


    // Метод Awake вызывается при инициализации объекта.
    private void Awake()
    {
        // Устанавливаем начальные значения.
        isInvicible = false;
        PlaySound(sounds[1]);
        Instance = this;
        lives = 5;
        health = lives;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    // Свойство для получения и установки состояния игрока.
    private States state
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    // Метод Update вызывается каждый кадр.
    private void Update()
    {
        // Проверяем, находится ли игрок на земле или на мобах.
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, RadiusGroundCheck, WhatIsGround);
        IsGroundedOnMobs = Physics2D.OverlapCircle(GroundCheck.position, RadiusGroundCheck, WhatIsMobs);
        isInWater = Physics2D.OverlapCircle(WaterCheck.position, RadiusWaterCheck, WhatIsWater);

        IsGroundUp = Physics2D.OverlapCircle(GroundUpCheck.position, RadiusGroundCheck, WhatIsGround);

        // Управляем состоянием игрока в зависимости от его положения и действий.
        if ((IsGrounded || IsGroundedOnMobs) && !IsAttacking)
        {
            sprite.flipX = false;
            state = States.AFK;
            IsRight = false;
        }
        if (IsGrounded == false && IsGroundedOnMobs == false && IsAttacking == false && IsSitting == false)
        {
            boxCollider5.enabled = false;
            Vector3 dir = transform.right * Input.GetAxis("HeroHorizontal");
            if (dir.x > 0.0f)
            {
                sprite.flipX = false;
                state = States.Jump;
            }
            if (dir.x < 0.0f)
            {
                sprite.flipX = true;
                state = States.Jump;
            }
            if (dir.x == 0.0f)
            {
                sprite.flipX = false;
                state = States.Jump3;
            }
        }
        if (Input.GetKeyUp(KeyCode.J) && IsRunning == false)
        {
            Attack();
        }
        IsRunning = Input.GetKey(KeyCode.LeftShift);
        IsSitting = Input.GetKey(KeyCode.LeftControl);
        if (Input.GetButton("HeroHorizontal") && !IsAttacking)
        {
            Run();
        }
        if (Input.GetButtonDown("Jump") && (IsGrounded || IsGroundedOnMobs) && !IsAttacking && !IsSitting)
        {
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && !IsGrounded && !IsGroundedOnMobs)
        {
            PlaySound(sounds[3]);
        }
        IsDeathZone = Physics2D.OverlapCircle(GroundCheck.position, RadiusGroundCheck, WhatIsDeathZone);
        if (IsDeathZone)
        {
            health = 0;
        }
        if (health == 0)
        {
            Die();
        }
        if (health > lives)
        {
            health = lives;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = alivehearts;
            else
                hearts[i].sprite = deadhearts;
        }

        // Управляем неуязвимостью игрока.
        if (isInvicible)
        {
            invicibleTimer = invicibleTimer - Time.deltaTime;
            if (invicibleTimer < 0)
            {
                sprite.color = originalColor;
                isInvicible = false;
            }
        }
        if (state != States.Sit && state != States.Run)
        {
            boxCollider1.enabled = true;
            boxCollider2.enabled = false;
            boxCollider3.enabled = false;
            boxCollider4.enabled = false;
            if (!IsRight && (IsGrounded || IsGroundedOnMobs))
            {
                boxCollider5.enabled = true;
            }
        }

        // Логика для получения урона в воде.
        if (isInWater)
        {
            timeInWater += Time.deltaTime;
            if (timeInWater >= maxTimeInWater && !isInvicible)
            {
                GetDamage();
            }
        }
        else
        {
            timeInWater = 0f; // Сброс времени не в воде
        }
    }

    // Метод для управления бегом игрока.
    private void Run()
    {
        float moveHorizontal = Input.GetAxis("HeroHorizontal");

        if (IsRunning == false && IsSitting == false && Crutch == false)
        {
            if (moveHorizontal > 0.0f && (IsGrounded || IsGroundedOnMobs))
            {
                HeroDir = "Right";
                state = States.Right;
                sprite.flipX = false;
                boxCollider5.enabled = false;
                IsRight = true;
            }
            if (moveHorizontal < 0.0f && (IsGrounded || IsGroundedOnMobs))
            {
                HeroDir = "Left";
                state = States.Right;
                sprite.flipX = true;
                boxCollider5.enabled = false;
                IsRight = true;
            }
            rb.velocity = new Vector2(moveHorizontal * speed * (isInWater ? waterSpeedMultiplier : 1f), rb.velocity.y);
        }
        if (IsRunning == true && IsSitting == false && Crutch == false)
        {
            if (moveHorizontal > 0.0f && (IsGrounded || IsGroundedOnMobs))
            {
                HeroDir = "Right";
                state = States.Run;
                sprite.flipX = false;
                boxCollider1.enabled = false;
                boxCollider5.enabled = false;
                boxCollider4.enabled = false;
                boxCollider3.enabled = true;
            }
            if (moveHorizontal < 0.0f && (IsGrounded || IsGroundedOnMobs))
            {
                HeroDir = "Left";
                state = States.Run;
                sprite.flipX = true;
                boxCollider1.enabled = false;
                boxCollider5.enabled = false;
                boxCollider3.enabled = false;
                boxCollider4.enabled = true;
            }
            rb.velocity = new Vector2(moveHorizontal * speed * 2f * (isInWater ? waterSpeedMultiplier : 1f), rb.velocity.y);
        }
        if ((IsSitting == true && IsRunning == false && moveHorizontal != 0) || (IsGroundUp == true && (IsGrounded || IsGroundedOnMobs)))
        {
            if (moveHorizontal > 0.0f && (IsGrounded || IsGroundedOnMobs))
            {
                HeroDir = "Right";
                state = States.Sit;
                sprite.flipX = true;
            }
            if (moveHorizontal < 0.0f && (IsGrounded || IsGroundedOnMobs))
            {
                HeroDir = "Left";
                state = States.Sit;
                sprite.flipX = false;
            }
            rb.velocity = new Vector2(moveHorizontal * speed * 0.5f * (isInWater ? waterSpeedMultiplier : 1f), rb.velocity.y);
            boxCollider1.enabled = false;
            boxCollider5.enabled = false;
            boxCollider2.enabled = true;
        }
        if (Crutch == true)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    // Корутина для анимации атаки.
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        IsAttacking = false;
    }

    // Корутина для перезарядки атаки.
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.2f);
        IsRecharged = true;
    }

    // Метод для атаки игрока.
    private void Attack()
    {
        if ((IsGrounded || IsGroundedOnMobs) && IsRecharged && Time.timeScale != 0)
        {
            float moveHorizontal = Input.GetAxis("HeroHorizontal");
            if (moveHorizontal > 0.0f)
            {
                sprite.flipX = false;
                state = States.Attack;
                IsAttacking = true;
                IsRecharged = false;
                Instantiate(bullet, BulletTransformRight.position, transform.rotation);
            }
            if (moveHorizontal < 0.0f)
            {
                sprite.flipX = true;
                state = States.Attack;
                IsAttacking = true;
                IsRecharged = false;
                Instantiate(bullet, BulletTransformLeft.position, transform.rotation);
            }
            if (moveHorizontal == 0.0f)
            {
                if (HeroDir == "Right")
                {
                    sprite.flipX = false;
                    state = States.Attack;
                    IsAttacking = true;
                    IsRecharged = false;
                    Instantiate(bullet, BulletTransformRight.position, transform.rotation);
                }
                if (HeroDir == "Left")
                {
                    sprite.flipX = true;
                    state = States.Attack;
                    IsAttacking = true;
                    IsRecharged = false;
                    Instantiate(bullet, BulletTransformLeft.position, transform.rotation);
                }
            }
            PlaySound(sounds[4]);

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
        else if ((!IsGrounded || !IsGroundedOnMobs || !IsRecharged) && Time.timeScale == 1)
        {
            PlaySound(sounds[3]);
        }
    }

    // Метод для прыжка игрока.
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * (isInWater ? (waterSpeedMultiplier * 1.5f) : 1f));
        if (!IsGrounded) state = States.Jump;
    }

    // Метод для смерти игрока.
    public void Die()
    {
        PauseButton.SetActive(false);
        PhisKey.IsDead = true;
        PausePanel.SetActive(true);
        PlaySound(sounds[2], destroyed: true);
        Destroy(this.gameObject);
    }

    // Метод для получения урона игроком.
    public void GetDamage()
    {
        health -= 1;
        PlaySound(sounds[0]);
        isInvicible = true;
        invicibleTimer = timeInvicible;
        StartCoroutine(Flash());
    }

    // Корутина для мигания игрока при получении урона.
    private IEnumerator Flash()
    {
        isInvicible = true;
        invicibleTimer = timeInvicible;

        while (isInvicible)
        {
            sprite.color = new Color(1, 0, 0, 0.5f); // Меняем цвет на красный с прозрачностью
            yield return new WaitForSeconds(0.1f);
            sprite.color = originalColor; // Возвращаем оригинальный цвет
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Метод для увеличения здоровья игрока.
    public void PlusHealth()
    {
        health += 1;
    }

    // Перечисление для состояний игрока.
    public enum States
    {
        AFK,
        Left,
        Right,
        Jump,
        Jump2,
        Jump3,
        Attack,
        Run,
        Sit
    }
}
