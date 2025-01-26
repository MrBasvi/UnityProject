using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

// Класс Buttons, наследующий MonoBehaviour, управляет кнопками в игре.
public class Buttons : MonoBehaviour
{
    // Панель паузы.
    public GameObject PausePanel;

    // Кнопка перезапуска.
    public GameObject RestartButton;

    // Кнопка продолжения.
    public GameObject ContuineuButton;

    // Кнопка паузы.
    public GameObject PauseButton;

    // Главное меню.
    public GameObject MainMenu;

    // Метод вызывается при нажатии кнопки перезапуска.
    public void RestartButtonPressed()
    {
        // Перезагружает текущую сцену.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Сбрасывает состояние паузы и смерти.
        PhisKey.IsPauseNow = false;
        PhisKey.IsDead = false;
    }

    // Метод вызывается при нажатии кнопки продолжения.
    public void ContinueButtonPressed()
    {
        // Активирует кнопку паузы.
        PauseButton.SetActive(true);

        // Сбрасывает состояние паузы.
        PhisKey.IsPauseNow = false;

        // Деактивирует панель паузы.
        PausePanel.SetActive(false);
    }

    // Метод вызывается при нажатии кнопки главного меню.
    public void MainMenuButtonPressed()
    {
        // Сбрасывает состояние смерти.
        PhisKey.IsDead = false;

        // Возвращает время к нормальной скорости.
        Time.timeScale = 1f;

        // Загружает главное меню (сцена с индексом 0).
        SceneManager.LoadScene(0);

        // Сбрасывает состояние паузы.
        PhisKey.IsPauseNow = false;
    }

    // Метод вызывается при нажатии кнопки паузы.
    public void PauseButtonPressed()
    {
        // Деактивирует кнопку паузы.
        PauseButton.SetActive(false);

        // Устанавливает состояние паузы.
        PhisKey.IsPauseNow = true;

        // Останавливает время.
        Time.timeScale = 0f;

        // Активирует панель паузы.
        PausePanel.SetActive(true);
    }
}
