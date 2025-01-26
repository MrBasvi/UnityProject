using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// ����� Hero, ����������� Sounds, ��������� ���������� ������ � ����.
public class Hero : Sounds
{
    // ������ �����.
    public GameObject PauseButton;

    // ������ �����.
    public GameObject PausePanel;

    // �������� �������� ������.
    [SerializeField] public float speed = 3f;

    // ���������� ������ ������.
    public int lives;

    // ���� ������ ������.
    static public float jumpForce = 15f;

    // ��������� Rigidbody2D ��� ���������� ������� ������.
    public Rigidbody2D rb;

    // ��������� Animator ��� ���������� ���������� ������.
    private Animator anim;

    // ��������� SpriteRenderer ��� ���������� �������� ������.
    public SpriteRenderer sprite;

    // ����� �������� ���������� �� �����.
    public Transform GroundCheck;

    // ����� �������� ���������� � ����.
    public Transform WaterCheck;

    // ����� ���� ��� �����.
    public LayerMask WhatIsGround;

    // ����� ���� ��� ���� ������.
    public LayerMask WhatIsDeathZone;

    // ����� ���� ��� �����.
    public LayerMask WhatIsMobs;

    // ����� ���� ��� ����.
    public LayerMask WhatIsWater;

    // ������ �������� ���������� �� �����.
    public float RadiusGroundCheck;

    // ������ �������� ���������� � ����.
    public float RadiusWaterCheck;

    // ����, �����������, ��������� �� ����� �� �����.
    public bool IsGrounded;

    // ����, �����������, ��������� �� ����� �� �����.
    public bool IsGroundedOnMobs;

    public Transform GroundUpCheck;
    public bool IsGroundUp;
    public float RadiusGroundUpCheck;

    // ����, �����������, ��������� �� ����� � ���� ������.
    private bool IsDeathZone;

    // ������ ����������� ������ ��� ����������� ��������.
    [SerializeField] private Image[] hearts;

    // ������� ���������� �������� ������.
    [SerializeField] static private int health;

    // ������ ������ ������.
    [SerializeField] private Sprite alivehearts;

    // ������ �������� ������.
    [SerializeField] private Sprite deadhearts;

    // ����� ������������ ����� ��������� �����.
    public float timeInvicible = 2f;

    // ����, �����������, �������� �� ����� ����������.
    public bool isInvicible;

    // ������ ������������.
    float invicibleTimer;

    // ����, �����������, ������� �� �����.
    public bool IsAttacking = false;

    // ����, �����������, ����������� �� �����.
    public bool IsRecharged = true;

    // ������ ����.
    public GameObject bullet;

    // ����� ������ ���� ������.
    public Transform BulletTransformRight;

    // ����� ������ ���� �����.
    public Transform BulletTransformLeft;

    // ����������� �������� ������.
    public string HeroDir = "Right";

    // ����, �����������, ����� �� �����.
    public bool IsRunning = false;

    // ����, �����������, ����� �� �����.
    public bool IsSitting = false;

    // ����, �����������, �������� �� ����� ������.
    public bool IsRight = false;

    // ������ �� ���������� BoxCollider2D.
    public BoxCollider2D boxCollider1;
    public BoxCollider2D boxCollider2;
    public BoxCollider2D boxCollider3;
    public BoxCollider2D boxCollider4;
    public BoxCollider2D boxCollider5;

    // ������������ ���� ������� ������.
    private Color originalColor;

    // ����, �����������, ���������� �� ����� �������.
    public bool Crutch;

    // ����������� �������� ��� ������� � ���������� ������.
    public static Hero Instance { get; set; }

    // ����, �����������, ��������� �� ����� � ����.
    private bool isInWater = false;

    // ��������� �������� � ����.
    public float waterSpeedMultiplier = 0.5f;

    // �����, ����������� � ����.
    private float timeInWater = 0f;

    // ������������ ����� � ���� ��� �����.
    private float maxTimeInWater = 10f;


    // ����� Awake ���������� ��� ������������� �������.
    private void Awake()
    {
        // ������������� ��������� ��������.
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

    // �������� ��� ��������� � ��������� ��������� ������.
    private States state
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    // ����� Update ���������� ������ ����.
    private void Update()
    {
        // ���������, ��������� �� ����� �� ����� ��� �� �����.
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, RadiusGroundCheck, WhatIsGround);
        IsGroundedOnMobs = Physics2D.OverlapCircle(GroundCheck.position, RadiusGroundCheck, WhatIsMobs);
        isInWater = Physics2D.OverlapCircle(WaterCheck.position, RadiusWaterCheck, WhatIsWater);

        IsGroundUp = Physics2D.OverlapCircle(GroundUpCheck.position, RadiusGroundCheck, WhatIsGround);

        // ��������� ���������� ������ � ����������� �� ��� ��������� � ��������.
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

        // ��������� ������������� ������.
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

        // ������ ��� ��������� ����� � ����.
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
            timeInWater = 0f; // ����� ������� �� � ����
        }
    }

    // ����� ��� ���������� ����� ������.
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

    // �������� ��� �������� �����.
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        IsAttacking = false;
    }

    // �������� ��� ����������� �����.
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.2f);
        IsRecharged = true;
    }

    // ����� ��� ����� ������.
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

    // ����� ��� ������ ������.
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * (isInWater ? (waterSpeedMultiplier * 1.5f) : 1f));
        if (!IsGrounded) state = States.Jump;
    }

    // ����� ��� ������ ������.
    public void Die()
    {
        PauseButton.SetActive(false);
        PhisKey.IsDead = true;
        PausePanel.SetActive(true);
        PlaySound(sounds[2], destroyed: true);
        Destroy(this.gameObject);
    }

    // ����� ��� ��������� ����� �������.
    public void GetDamage()
    {
        health -= 1;
        PlaySound(sounds[0]);
        isInvicible = true;
        invicibleTimer = timeInvicible;
        StartCoroutine(Flash());
    }

    // �������� ��� ������� ������ ��� ��������� �����.
    private IEnumerator Flash()
    {
        isInvicible = true;
        invicibleTimer = timeInvicible;

        while (isInvicible)
        {
            sprite.color = new Color(1, 0, 0, 0.5f); // ������ ���� �� ������� � �������������
            yield return new WaitForSeconds(0.1f);
            sprite.color = originalColor; // ���������� ������������ ����
            yield return new WaitForSeconds(0.1f);
        }
    }

    // ����� ��� ���������� �������� ������.
    public void PlusHealth()
    {
        health += 1;
    }

    // ������������ ��� ��������� ������.
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
