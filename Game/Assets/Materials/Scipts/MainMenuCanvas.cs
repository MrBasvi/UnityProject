using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ����� MainMenuCanvas, ����������� MonoBehaviour, ��������� ������� ���� ����.
public class MainMenuCanvas : MonoBehaviour
{
    // ������� ����.
    public GameObject Logo;

    // ������ � ��������.
    public GameObject Levels;

    // ������ ������ ����.
    public GameObject PlayButton;

    // ������ ��������.
    public GameObject SettingsButton;

    // ������ ������ �� ����.
    public GameObject ExitButtonMenu;

    // ������ ��������.
    public GameObject BackButton;

    // ������ ��������.
    public GameObject Settings;

    // ����������� �������� ��� ������� � ���������� ������.
    public static MainMenuCanvas Instance { get; set; }

    // ����� Start ���������� ��� ������������� �������.
    private void Start()
    {
        // ������������� ������� ��������� ������.
        Instance = this;
    }

    // ����� Update ���������� ������ ����.
    void Update()
    {
        // � ������ ������ ����� ����, �� �� ����� ���� ����������� ��� ���������� ���������.
    }

    // ����� ���������� ��� ������� ������ ������ ����.
    public void PlayButtonPressed()
    {
        // �������� �������, ������ ������ ����, �������� � ������.
        Logo.SetActive(false);
        PlayButton.SetActive(false);
        SettingsButton.SetActive(false);
        ExitButtonMenu.SetActive(false);

        // ���������� ������ � �������� � ������ ��������.
        Levels.SetActive(true);
        BackButton.SetActive(true);
    }

    // ����� ���������� ��� ������� ������ ������ �� ����.
    public void ExitButtonMenuPressed()
    {
        // ��������� ���������� ����������.
        Application.Quit();
    }

    // ����� ���������� ��� ������� ������ ��������.
    public void SettingsButtonPressed()
    {
        // �������� �������, ������ ������ ����, �������� � ������.
        Logo.SetActive(false);
        PlayButton.SetActive(false);
        SettingsButton.SetActive(false);
        ExitButtonMenu.SetActive(false);

        // ���������� ������ ��������.
        Settings.SetActive(true);
    }

    // ����� ���������� ��� ������� ������ ��������.
    public void BackButtonPressed()
    {
        // ���������� �������, ������ ������ ����, �������� � ������.
        Logo.SetActive(true);
        PlayButton.SetActive(true);
        SettingsButton.SetActive(true);
        ExitButtonMenu.SetActive(true);

        // �������� ������ � �������� � ������ ��������.
        Levels.SetActive(false);
        BackButton.SetActive(false);
    }

    // ����� ���������� ��� ������� ������ ������ �� ��������.
    public void ExitButtonSettingsPressed()
    {
        // ���������� �������, ������ ������ ����, �������� � ������.
        Logo.SetActive(true);
        PlayButton.SetActive(true);
        SettingsButton.SetActive(true);
        ExitButtonMenu.SetActive(true);

        // �������� ������ ��������.
        Settings.SetActive(false);
    }
}
