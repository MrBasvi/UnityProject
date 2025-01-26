using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Класс Door, наследующий MonoBehaviour, управляет поведением двери в игре.
public class Door : MonoBehaviour
{
    // Компонент Animator для управления анимациями двери.
    private Animator anim;

    // Метод Awake вызывается при инициализации объекта.
    void Awake()
    {
        // Получаем компонент Animator.
        anim = GetComponent<Animator>();
    }

    // Свойство для получения и установки состояния двери.
    private States state
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    // Метод OnTriggerEnter2D вызывается при столкновении с другим объектом.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если столкновение произошло с объектом, помеченным тегом "Player", и дверь открыта.
        if (collision.CompareTag("Player") && state == States.Open)
        {
            // Переходим на следующий уровень.
            NextLevel();
        }
    }

    // Метод Update вызывается каждый кадр.
    void Update()
    {
        // Если количество собранных монет равно количеству детей.
        if (CoinCollect.Instance.money == ChildCounter.Instance.childCount)
        {
            // Открываем дверь.
            state = States.Open;
        }
    }

    // Метод для перехода на следующий уровень.
    public void NextLevel()
    {
        // Получаем текущий индекс сцены.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Загружаем следующую сцену (текущая сцена + 1).
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    // Перечисление для состояний двери.
    public enum States
    {
        Close,
        Open
    }
}
