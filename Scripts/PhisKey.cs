using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Класс PhisKey, наследующий Sounds, управляет физическими ключами и состоянием игры.
public class PhisKey : Sounds
{
    // Кнопка паузы.
    public GameObject PauseButton;

    // Флаг, указывающий, находится ли игра на паузе.
    static public bool IsPauseNow = false;

    // Флаг, указывающий, мертв ли игрок.
    static public bool IsDead = false;

    // Панель паузы.
    public GameObject PausePanel;

    // Текст помощи.
    public GameObject HelpText;

    // Панель помощи.
    public GameObject HelpPanel;

    // Флаг, указывающий, отображается ли помощь.
    public bool IsHelp = false;

    // Метод Start вызывается при инициализации объекта.
    void Start()
    {
        // В данном случае метод пуст, но он может быть использован для инициализации.
    }

    // Метод Update вызывается каждый кадр.
    void Update()
    {
        // Если игрок мертв, останавливаем время.
        if (IsDead)
        {
            Time.timeScale = 0f;
        }

        // Если нажата клавиша R, перезагружаем текущую сцену и сбрасываем состояние игры.
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            IsDead = false;
            IsPauseNow = false;
        }

        // Если нажата клавиша Escape, переключаем состояние паузы.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsPauseNow = !IsPauseNow;
            PauseButton.SetActive(true);
            if (IsPauseNow)
            {
                PauseButton.SetActive(false);
                PlaySound(sounds[0]);
            }
        }

        // Если нажата клавиша F4, переключаем состояние отображения помощи.
        if (Input.GetKeyDown(KeyCode.F4))
        {
            IsHelp = !IsHelp;
        }

        // Управляем отображением панели помощи и текста помощи.
        if (IsHelp)
        {
            HelpText.SetActive(false);
            HelpPanel.SetActive(true);
        }
        else
        {
            HelpText.SetActive(true);
            HelpPanel.SetActive(false);
        }

        // Управляем состоянием паузы и смерти игрока.
        if (IsPauseNow == true || IsDead == true)
        {
            Time.timeScale = 0f;
            if (IsDead == false && IsPauseNow == true)
            {
                PausePanel.SetActive(true);
            }
            Hero.jumpForce = 0f;
        }
        else
        {
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
            Hero.jumpForce = 15f;
        }
    }
}
