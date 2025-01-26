using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

// ����� Buttons, ����������� MonoBehaviour, ��������� �������� � ����.
public class Buttons : MonoBehaviour
{
    // ������ �����.
    public GameObject PausePanel;

    // ������ �����������.
    public GameObject RestartButton;

    // ������ �����������.
    public GameObject ContuineuButton;

    // ������ �����.
    public GameObject PauseButton;

    // ������� ����.
    public GameObject MainMenu;

    // ����� ���������� ��� ������� ������ �����������.
    public void RestartButtonPressed()
    {
        // ������������� ������� �����.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // ���������� ��������� ����� � ������.
        PhisKey.IsPauseNow = false;
        PhisKey.IsDead = false;
    }

    // ����� ���������� ��� ������� ������ �����������.
    public void ContinueButtonPressed()
    {
        // ���������� ������ �����.
        PauseButton.SetActive(true);

        // ���������� ��������� �����.
        PhisKey.IsPauseNow = false;

        // ������������ ������ �����.
        PausePanel.SetActive(false);
    }

    // ����� ���������� ��� ������� ������ �������� ����.
    public void MainMenuButtonPressed()
    {
        // ���������� ��������� ������.
        PhisKey.IsDead = false;

        // ���������� ����� � ���������� ��������.
        Time.timeScale = 1f;

        // ��������� ������� ���� (����� � �������� 0).
        SceneManager.LoadScene(0);

        // ���������� ��������� �����.
        PhisKey.IsPauseNow = false;
    }

    // ����� ���������� ��� ������� ������ �����.
    public void PauseButtonPressed()
    {
        // ������������ ������ �����.
        PauseButton.SetActive(false);

        // ������������� ��������� �����.
        PhisKey.IsPauseNow = true;

        // ������������� �����.
        Time.timeScale = 0f;

        // ���������� ������ �����.
        PausePanel.SetActive(true);
    }
}
