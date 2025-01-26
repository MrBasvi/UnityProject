using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс Bullet, наследующий MonoBehaviour, представляет пулю в игре.
public class Bullet : MonoBehaviour
{
    // Компонент Rigidbody2D для управления физикой пули.
    Rigidbody2D rb;

    // Скорость пули.
    public float speed;

    // Время, через которое пуля будет уничтожена.
    public int destroy;

    // Компонент SpriteRenderer для управления спрайтом пули.
    public SpriteRenderer sprite;

    // Метод Start вызывается при инициализации объекта.
    private void Start()
    {
        // Получаем компонент SpriteRenderer.
        sprite = GetComponent<SpriteRenderer>();

        // Получаем компонент Rigidbody2D.
        rb = GetComponent<Rigidbody2D>();

        // Если спрайт героя отражен по оси X, меняем направление пули и отражаем её спрайт.
        if (Hero.Instance.sprite.flipX == true)
        {
            speed = -speed;
            sprite.flipX = true;
        }

        // Устанавливаем скорость пули.
        rb.velocity = transform.right * speed;

        // Вызываем метод DestroyTime через заданное время.
        Invoke("DestroyTime", destroy);
    }

    // Метод Update вызывается каждый кадр.
    void Update()
    {
        // Перемещаем пулю вправо с заданной скоростью.
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    // Метод DestroyTime уничтожает пулю.
    void DestroyTime()
    {
        Destroy(gameObject);
    }

    // Метод OnTriggerEnter2D вызывается при столкновении пули с другим объектом.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если пуля столкнулась с мобом или землей, уничтожаем её.
        if (collision.CompareTag("Mobs") || collision.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
