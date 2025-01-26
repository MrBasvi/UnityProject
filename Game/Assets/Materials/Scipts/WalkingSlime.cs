using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� WalkingSlime, ����������� Sounds, ��������� ���������� ������ � ����.
public class WalkingSlime : Sounds
{
    // ����������� �������� ������.
    private Vector3 dir;

    // ���������� ������ ������.
    private int lives = 2;

    // ��������� SpriteRenderer ��� ���������� ��������.
    private SpriteRenderer sprite;

    // ��������� Rigidbody2D ��� ���������� �������.
    private Rigidbody2D rb;

    // ����� �������� ���������� �� ����� �����.
    public Transform GroundCheckLeft;

    // ����� �������� ���������� �� ����� ������.
    public Transform GroundCheckRight;

    // ����� ���� ��� �����.
    public LayerMask WhatIsGround;

    // ����� ���� ��� �����.
    public LayerMask WhatIsMob;

    // ����� ���� ��� ���� ������.
    public LayerMask WhatIsDeathZone;

    // ������ �������� ���������� �� �����.
    public float RadiusGroundCheck;

    // ����, �����������, ��������� �� ����� �� ����� �����.
    private bool IsGroundedLeft;

    // ����, �����������, ��������� �� ����� �� ����� ������.
    private bool IsGroundedRight;

    // ����, �����������, ��������� �� ��� �����.
    private bool IsMobLeft;

    // ����, �����������, ��������� �� ��� ������.
    private bool IsMobRight;

    // ����, �����������, ����� �� ����� �������������.
    private bool CanFlip;

    // ����, �����������, ��������� �� ����� � ���� ������.
    private bool IsDeathZone;

    // ����������� �������� ��� ������� � ���������� ������.
    public static WalkingSlime Instance { get; set; }

    // ����� Start ���������� ��� ������������� �������.
    private void Start()
    {
        // ������������� ��������� ��������.
        CanFlip = true;
        dir = transform.right;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // ����� ��� �������� ������.
    private void Move()
    {
        // ���������� ������ � ����������� dir.
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
    }

    // ����� Update ���������� ������ ����.
    private void Update()
    {
        // ���������, ��������� �� ����� � ���� ������.
        IsDeathZone = Physics2D.OverlapCircle(GroundCheckRight.position, RadiusGroundCheck, WhatIsDeathZone);
        if (IsDeathZone)
        {
            lives = 0;
        }

        // ��������� ������� ����� � ����� ����� � ������.
        IsMobLeft = Physics2D.OverlapCircle(GroundCheckLeft.position, RadiusGroundCheck, WhatIsMob);
        IsMobRight = Physics2D.OverlapCircle(GroundCheckRight.position, RadiusGroundCheck, WhatIsMob);
        IsGroundedRight = Physics2D.OverlapCircle(GroundCheckRight.position, RadiusGroundCheck, WhatIsGround);
        IsGroundedLeft = Physics2D.OverlapCircle(GroundCheckLeft.position, RadiusGroundCheck, WhatIsGround);

        // �������������� ������, ���� �� ������ ���� ��������� ��� ���������� � �����.
        if (((IsGroundedRight == false && IsGroundedLeft == true) || IsMobRight == true) && CanFlip == true)
        {
            flip();
        }
        if (((IsGroundedLeft == false && IsGroundedRight == true) || IsMobLeft == true) && CanFlip == true)
        {
            flip();
        }

        // ������� ������.
        Move();
    }

    // ����� ���������� ��� ������������ � ������ ��������.
    void OnCollisionStay2D(Collision2D collision)
    {
        // ���� ����� �� �����.
        if (PhisKey.IsDead == false)
        {
            // ���� ������������ ��������� � ������� � ����� �� ��������.
            if (collision.gameObject == Hero.Instance.gameObject && Hero.Instance.isInvicible == false)
            {
                // ����� �������� ����.
                Hero.Instance.GetDamage();
                GetDamage();
            }

            // ���� � ������ �� �������� ������.
            if (lives < 1)
            {
                // ������������� ���� � ���������� ������.
                PlaySound(sounds[0], destroyed: true);
                Destroy(this.gameObject);
            }
        }
    }

    // ����� ���������� ��� ����� � �������.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ������������ ��������� � �����.
        if (collision.CompareTag("Bullet"))
        {
            GetDamage();
        }
    }

    // ����� ��� ��������� ����� �������.
    public void GetDamage()
    {
        lives -= 1;
    }

    // �������� ��� �������� ������ � ��������� ���.
    private IEnumerator Sleep()
    {
        yield return new WaitForSeconds(2f);
        CanFlip = true;
    }

    // ����� ��� ���������� ������.
    public void flip()
    {
        // ������ ����������� �������� � �������������� ������.
        dir = -dir;
        sprite.flipX = !sprite.flipX;
        CanFlip = false;
        StartCoroutine(Sleep());
    }
}
