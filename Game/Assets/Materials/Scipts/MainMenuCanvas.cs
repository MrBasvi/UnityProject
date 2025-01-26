using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Класс MainMenuCanvas, наследующий MonoBehaviour, управляет главным меню игры.
public class MainMenuCanvas : MonoBehaviour
{
    // Логотип игры.
    public GameObject Logo;

    // Панель с уровнями.
    public GameObject Levels;

    // Кнопка начала игры.
    public GameObject PlayButton;

    // Кнопка настроек.
    public GameObject SettingsButton;

    // Кнопка выхода из игры.
    public GameObject ExitButtonMenu;

    // Кнопка возврата.
    public GameObject BackButton;

    // Панель настроек.
    public GameObject Settings;

    // Статическое свойство для доступа к экземпляру класса.
    public static MainMenuCanvas Instance { get; set; }

    // Метод Start вызывается при инициализации объекта.
    private void Start()
    {
        // Устанавливаем текущий экземпляр класса.
        Instance = this;
    }

    // Метод Update вызывается каждый кадр.
    void Update()
    {
        // В данном случае метод пуст, но он может быть использован для обновления состояния.
    }

    // Метод вызывается при нажатии кнопки начала игры.
    public void PlayButtonPressed()
    {
        // Скрываем логотип, кнопки начала игры, настроек и выхода.
        Logo.SetActive(false);
        PlayButton.SetActive(false);
        SettingsButton.SetActive(false);
        ExitButtonMenu.SetActive(false);

        // Отображаем панель с уровнями и кнопку возврата.
        Levels.SetActive(true);
        BackButton.SetActive(true);
    }

    // Метод вызывается при нажатии кнопки выхода из игры.
    public void ExitButtonMenuPressed()
    {
        // Завершает выполнение приложения.
        Application.Quit();
    }

    // Метод вызывается при нажатии кнопки настроек.
    public void SettingsButtonPressed()
    {
        // Скрываем логотип, кнопки начала игры, настроек и выхода.
        Logo.SetActive(false);
        PlayButton.SetActive(false);
        SettingsButton.SetActive(false);
        ExitButtonMenu.SetActive(false);

        // Отображаем панель настроек.
        Settings.SetActive(true);
    }

    // Метод вызывается при нажатии кнопки возврата.
    public void BackButtonPressed()
    {
        // Отображаем логотип, кнопки начала игры, настроек и выхода.
        Logo.SetActive(true);
        PlayButton.SetActive(true);
        SettingsButton.SetActive(true);
        ExitButtonMenu.SetActive(true);

        // Скрываем панель с уровнями и кнопку возврата.
        Levels.SetActive(false);
        BackButton.SetActive(false);
    }

    // Метод вызывается при нажатии кнопки выхода из настроек.
    public void ExitButtonSettingsPressed()
    {
        // Отображаем логотип, кнопки начала игры, настроек и выхода.
        Logo.SetActive(true);
        PlayButton.SetActive(true);
        SettingsButton.SetActive(true);
        ExitButtonMenu.SetActive(true);

        // Скрываем панель настроек.
        Settings.SetActive(false);
    }
}
