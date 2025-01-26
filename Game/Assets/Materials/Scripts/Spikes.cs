using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс Spikes, наследующий MonoBehaviour, управляет поведением шипов в игре.
public class Spikes : MonoBehaviour
{
    // Компонент SpriteRenderer для управления спрайтом.
    public SpriteRenderer sprite;

    // Ссылки на компоненты BoxCollider2D.
    public BoxCollider2D boxCollider1;
    public BoxCollider2D boxCollider2;
    public BoxCollider2D boxCollider3;
    public BoxCollider2D boxCollider4;

    // Флаг, указывающий, находится ли объект в состоянии сна.
    private bool IsSleep;

    // Компонент Animator для управления анимациями.
    private Animator anim;

    // Имя последнего спрайта.
    private string lastSprite;

    // Текущий спрайт.
    Sprite currentSprite;

    // Метод Start вызывается перед первым обновлением кадра.
    void Start()
    {
        // Получаем компоненты Animator и SpriteRenderer.
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        // Отключаем все BoxCollider2D.
        boxCollider1.enabled = false;
        boxCollider2.enabled = false;
        boxCollider3.enabled = false;
        boxCollider4.enabled = false;

        // Устанавливаем начальные значения.
        IsSleep = false;
        currentSprite = sprite.sprite;
        lastSprite = currentSprite.name;
    }

    // Метод Update вызывается каждый кадр.
    void Update()
    {
        // Если текущий спрайт изменился.
        if (currentSprite.name != lastSprite)
        {
            lastSprite = currentSprite.name;
        }

        // Обновляем текущий спрайт.
        currentSprite = sprite.sprite;

        // Если объект не находится в состоянии сна.
        if (IsSleep == false)
        {
            // Устанавливаем нормальную скорость анимации.
            anim.speed = 1;

            // Изменяем состояние коллайдеров в зависимости от текущего спрайта.
            ChangeCollider(currentSprite, lastSprite);
        }

        // Если объект находится в состоянии сна.
        if (IsSleep == true)
        {
            // Останавливаем анимацию.
            anim.speed = 0;
        }
    }

    // Корутина для перевода объекта в состояние сна.
    private IEnumerator Sleep()
    {
        yield return new WaitForSeconds(2f);
        IsSleep = false;
    }

    // Метод для изменения состояния коллайдеров в зависимости от текущего спрайта.
    private void ChangeCollider(Sprite currentSprite, string lastSprite)
    {
        // В зависимости от имени текущего спрайта включаем или отключаем соответствующие коллайдеры.
        if (currentSprite.name == "Spikes_0" && lastSprite == "Spikes_7")
        {
            boxCollider1.enabled = false;
            IsSleep = true;
            StartCoroutine(Sleep());
        }
        if (currentSprite.name == "Spikes_1")
        {
            boxCollider1.enabled = true;
        }
        if (currentSprite.name == "Spikes_2")
        {
            boxCollider1.enabled = false;
            boxCollider2.enabled = true;
        }
        if (currentSprite.name == "Spikes_3")
        {
            boxCollider2.enabled = false;
            boxCollider3.enabled = true;
        }
        if (currentSprite.name == "Spikes_4")
        {
            boxCollider3.enabled = false;
            boxCollider4.enabled = true;
        }
        if (currentSprite.name == "Spikes_5")
        {
            boxCollider4.enabled = false;
            boxCollider3.enabled = true;
        }
        if (currentSprite.name == "Spikes_6")
        {
            boxCollider3.enabled = false;
            boxCollider2.enabled = true;
        }
        if (currentSprite.name == "Spikes_7")
        {
            boxCollider2.enabled = false;
            boxCollider1.enabled = true;
        }
    }

    // Метод вызывается при столкновении с другим объектом.
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Если столкновение произошло с игроком и игрок не неуязвим.
        if (collision.gameObject == Hero.Instance.gameObject && Hero.Instance.isInvicible == false)
        {
            // Игрок получает урон.
            Hero.Instance.GetDamage();
        }
    }
}
