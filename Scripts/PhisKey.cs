using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ����� PhisKey, ����������� Sounds, ��������� ����������� ������� � ���������� ����.
public class PhisKey : Sounds
{
    // ������ �����.
    public GameObject PauseButton;

    // ����, �����������, ��������� �� ���� �� �����.
    static public bool IsPauseNow = false;

    // ����, �����������, ����� �� �����.
    static public bool IsDead = false;

    // ������ �����.
    public GameObject PausePanel;

    // ����� ������.
    public GameObject HelpText;

    // ������ ������.
    public GameObject HelpPanel;

    // ����, �����������, ������������ �� ������.
    public bool IsHelp = false;

    // ����� Start ���������� ��� ������������� �������.
    void Start()
    {
        // � ������ ������ ����� ����, �� �� ����� ���� ����������� ��� �������������.
    }

    // ����� Update ���������� ������ ����.
    void Update()
    {
        // ���� ����� �����, ������������� �����.
        if (IsDead)
        {
            Time.timeScale = 0f;
        }

        // ���� ������ ������� R, ������������� ������� ����� � ���������� ��������� ����.
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            IsDead = false;
            IsPauseNow = false;
        }

        // ���� ������ ������� Escape, ����������� ��������� �����.
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

        // ���� ������ ������� F4, ����������� ��������� ����������� ������.
        if (Input.GetKeyDown(KeyCode.F4))
        {
            IsHelp = !IsHelp;
        }

        // ��������� ������������ ������ ������ � ������ ������.
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

        // ��������� ���������� ����� � ������ ������.
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
