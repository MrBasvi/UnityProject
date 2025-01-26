using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Класс CoinCollect, наследующий Sounds, управляет сбором монет в игре.
public class CoinCollect : Sounds
{
    // Текстовое поле для отображения количества собранных монет.
    public Text text;

    // Количество собранных монет.
    public int money = 0;

    // Статическое свойство для доступа к экземпляру класса.
    public static CoinCollect Instance { get; set; }

    // Метод OnTriggerEnter2D вызывается при столкновении с другим объектом.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если столкновение произошло с объектом, помеченным тегом "Money".
        if (collision.CompareTag("Money"))
        {
            // Воспроизводит звук сбора монеты.
            PlaySound(sounds[0]);

            // Уничтожает объект монеты.
            Destroy(collision.gameObject);

            // Увеличивает количество собранных монет.
            money++;

            // Обновляет текстовое поле с количеством собранных монет.
            text.text = money.ToString() + " / " + ChildCounter.Instance.childCount.ToString();
        }
    }

    // Метод Start вызывается при инициализации объекта.
    void Start()
    {
        // Устанавливает текущий экземпляр класса.
        Instance = this;

        // Инициализирует текстовое поле с количеством собранных монет.
        text.text = money.ToString() + " / " + ChildCounter.Instance.childCount.ToString();
    }
}

//───▐▀▄──────▄▀▌───▄▄▄▄▄▄▄
//───▌▒▒▀▄▄▄▄▀▒▒▐▄▀▀▒██▒██▒▀▀▄
//──▐▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▀▄
//──▌▒▒▒▒▒▒▒▒▒▒▒▒▒▄▒▒▒▒▒▒▒▒▒▒▒▒▒▀▄
//▀█▒▒█▌▒▒█▒▒▐█▒▒▀▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▌
//▀▌▒▒▒▒▒▀▒▀▒▒▒▒▒▀▀▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▐ ▄▄
//▐▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▄█▒█
//▐▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█▀
//──▐▄▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▄▌
//────▀▄▄▀▀▀▀▄▄▀▀▀▀▀▀▄▄▀▀▀▀▀▀▄▄▀