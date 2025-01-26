using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс WalkingSlime, наследующий Sounds, управляет поведением слайма в игре.
public class WalkingSlime : Sounds
{
    // Направление движения слайма.
    private Vector3 dir;

    // Количество жизней слайма.
    private int lives = 2;

    // Компонент SpriteRenderer для управления спрайтом.
    private SpriteRenderer sprite;

    // Компонент Rigidbody2D для управления физикой.
    private Rigidbody2D rb;

    // Точка проверки нахождения на земле слева.
    public Transform GroundCheckLeft;

    // Точка проверки нахождения на земле справа.
    public Transform GroundCheckRight;

    // Маска слоя для земли.
    public LayerMask WhatIsGround;

    // Маска слоя для мобов.
    public LayerMask WhatIsMob;

    // Маска слоя для зоны смерти.
    public LayerMask WhatIsDeathZone;

    // Радиус проверки нахождения на земле.
    public float RadiusGroundCheck;

    // Флаг, указывающий, находится ли слайм на земле слева.
    private bool IsGroundedLeft;

    // Флаг, указывающий, находится ли слайм на земле справа.
    private bool IsGroundedRight;

    // Флаг, указывающий, находится ли моб слева.
    private bool IsMobLeft;

    // Флаг, указывающий, находится ли моб справа.
    private bool IsMobRight;

    // Флаг, указывающий, может ли слайм перевернуться.
    private bool CanFlip;

    // Флаг, указывающий, находится ли слайм в зоне смерти.
    private bool IsDeathZone;

    // Статическое свойство для доступа к экземпляру класса.
    public static WalkingSlime Instance { get; set; }

    // Метод Start вызывается при инициализации объекта.
    private void Start()
    {
        // Устанавливаем начальные значения.
        CanFlip = true;
        dir = transform.right;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Метод для движения слайма.
    private void Move()
    {
        // Перемещаем слайма в направлении dir.
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
    }

    // Метод Update вызывается каждый кадр.
    private void Update()
    {
        // Проверяем, находится ли слайм в зоне смерти.
        IsDeathZone = Physics2D.OverlapCircle(GroundCheckRight.position, RadiusGroundCheck, WhatIsDeathZone);
        if (IsDeathZone)
        {
            lives = 0;
        }

        // Проверяем наличие мобов и земли слева и справа.
        IsMobLeft = Physics2D.OverlapCircle(GroundCheckLeft.position, RadiusGroundCheck, WhatIsMob);
        IsMobRight = Physics2D.OverlapCircle(GroundCheckRight.position, RadiusGroundCheck, WhatIsMob);
        IsGroundedRight = Physics2D.OverlapCircle(GroundCheckRight.position, RadiusGroundCheck, WhatIsGround);
        IsGroundedLeft = Physics2D.OverlapCircle(GroundCheckLeft.position, RadiusGroundCheck, WhatIsGround);

        // Переворачиваем слайма, если он достиг края платформы или столкнулся с мобом.
        if (((IsGroundedRight == false && IsGroundedLeft == true) || IsMobRight == true) && CanFlip == true)
        {
            flip();
        }
        if (((IsGroundedLeft == false && IsGroundedRight == true) || IsMobLeft == true) && CanFlip == true)
        {
            flip();
        }

        // Двигаем слайма.
        Move();
    }

    // Метод вызывается при столкновении с другим объектом.
    void OnCollisionStay2D(Collision2D collision)
    {
        // Если игрок не мертв.
        if (PhisKey.IsDead == false)
        {
            // Если столкновение произошло с игроком и игрок не неуязвим.
            if (collision.gameObject == Hero.Instance.gameObject && Hero.Instance.isInvicible == false)
            {
                // Игрок получает урон.
                Hero.Instance.GetDamage();
                GetDamage();
            }

            // Если у слайма не осталось жизней.
            if (lives < 1)
            {
                // Воспроизводим звук и уничтожаем слайма.
                PlaySound(sounds[0], destroyed: true);
                Destroy(this.gameObject);
            }
        }
    }

    // Метод вызывается при входе в триггер.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если столкновение произошло с пулей.
        if (collision.CompareTag("Bullet"))
        {
            GetDamage();
        }
    }

    // Метод для получения урона слаймом.
    public void GetDamage()
    {
        lives -= 1;
    }

    // Корутина для перевода слайма в состояние сна.
    private IEnumerator Sleep()
    {
        yield return new WaitForSeconds(2f);
        CanFlip = true;
    }

    // Метод для переворота слайма.
    public void flip()
    {
        // Меняем направление движения и переворачиваем спрайт.
        dir = -dir;
        sprite.flipX = !sprite.flipX;
        CanFlip = false;
        StartCoroutine(Sleep());
    }
}
