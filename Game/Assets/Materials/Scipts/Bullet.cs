using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� Bullet, ����������� MonoBehaviour, ������������ ���� � ����.
public class Bullet : MonoBehaviour
{
    // ��������� Rigidbody2D ��� ���������� ������� ����.
    Rigidbody2D rb;

    // �������� ����.
    public float speed;

    // �����, ����� ������� ���� ����� ����������.
    public int destroy;

    // ��������� SpriteRenderer ��� ���������� �������� ����.
    public SpriteRenderer sprite;

    // ����� Start ���������� ��� ������������� �������.
    private void Start()
    {
        // �������� ��������� SpriteRenderer.
        sprite = GetComponent<SpriteRenderer>();

        // �������� ��������� Rigidbody2D.
        rb = GetComponent<Rigidbody2D>();

        // ���� ������ ����� ������� �� ��� X, ������ ����������� ���� � �������� � ������.
        if (Hero.Instance.sprite.flipX == true)
        {
            speed = -speed;
            sprite.flipX = true;
        }

        // ������������� �������� ����.
        rb.velocity = transform.right * speed;

        // �������� ����� DestroyTime ����� �������� �����.
        Invoke("DestroyTime", destroy);
    }

    // ����� Update ���������� ������ ����.
    void Update()
    {
        // ���������� ���� ������ � �������� ���������.
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    // ����� DestroyTime ���������� ����.
    void DestroyTime()
    {
        Destroy(gameObject);
    }

    // ����� OnTriggerEnter2D ���������� ��� ������������ ���� � ������ ��������.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ���� ����������� � ����� ��� ������, ���������� �.
        if (collision.CompareTag("Mobs") || collision.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
