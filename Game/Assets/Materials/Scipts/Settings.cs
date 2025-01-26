using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ����� SettingsManager, ����������� MonoBehaviour, ��������� ����������� ����.
public class SettingsManager : MonoBehaviour
{
    // ���������� ������ ��� ������ ���������� ������.
    public Dropdown resolutionDropdown;

    // ������������� ��� ������ �������������� ������.
    public Toggle fullScreenToggle;

    // ������ ��� ���������� ��������.
    public Button saveButton;

    // ������ ��� ������ �� ��������.
    public Button exitButton;

    // ������ ��������� ���������� ������.
    Resolution[] resolutions;

    // ����� Awake ���������� ��� ������������� �������.
    void Awake()
    {
        // �������������� ��������� ���������� � ��������� ����������� ���������.
        InitializeResolutions();
        LoadSettings();
    }

    // ����� ��� ������������� ��������� ����������.
    void InitializeResolutions()
    {
        // ������� ������ ����� ����� ����������� �����.
        resolutionDropdown.ClearOptions();

        // ������� ������ ����� ��� ����� ����������.
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        // ��������� ������ ����� ����������.
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate.ToString();
            options.Add(option);

            // ���������� ������� ����������.
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].refreshRate.Equals(Screen.currentResolution.refreshRate))
            {
                currentResolutionIndex = i;
            }
        }

        // ��������� ����� � ���������� ������ � ������������� ������� ����������.
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // ����� ��� ��������� ���������� ������.
    public void SetResolution(int resolutionIndex)
    {
        // ��������� �������� ��� ��������� ����������.
        StartCoroutine(SetResolutionCoroutine(resolutionIndex));
    }

    // �������� ��� ��������� ���������� ������.
    IEnumerator SetResolutionCoroutine(int resolutionIndex)
    {
        // ��������� ��������� �������� ����� ���������� ����������.
        yield return new WaitForEndOfFrame();

        // ������������� ����� ����������.
        Resolution resolution = resolutions[resolutionIndex];
        FullScreenMode fullScreenMode = fullScreenToggle.isOn ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
        Screen.SetResolution(resolution.width, resolution.height, fullScreenMode, resolution.refreshRate);
    }

    // ����� ��� ��������� �������������� ������.
    public void SetFullScreen(bool isFullScreen)
    {
        FullScreenMode fullScreenMode = isFullScreen ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
        Screen.fullScreenMode = fullScreenMode;
    }

    // ����� ��� ���������� ��������.
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullScreenPreference", System.Convert.ToInt32(Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen));
        PlayerPrefs.Save();
    }

    // ����� ��� �������� ����������� ��������.
    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("ResolutionPreference"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
            SetResolution(resolutionDropdown.value);
        }
        if (PlayerPrefs.HasKey("FullScreenPreference"))
        {
            bool isFullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenPreference"));
            fullScreenToggle.isOn = isFullScreen;
            SetFullScreen(isFullScreen);
        }
    }

    // ����� ��� ������ �� ���� ��������.
    public void ExitSettings()
    {
        // ������ ��� ������ �� ���� ��������.
        gameObject.SetActive(false);
        MainMenuCanvas.Instance.Logo.SetActive(true);
        MainMenuCanvas.Instance.PlayButton.SetActive(true);
        MainMenuCanvas.Instance.SettingsButton.SetActive(true);
        MainMenuCanvas.Instance.ExitButtonMenu.SetActive(true);
    }

    // �������� ������� � ��������� UI.
    void OnEnable()
    {
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
        fullScreenToggle.onValueChanged.AddListener(SetFullScreen);
        saveButton.onClick.AddListener(SaveSettings);
        exitButton.onClick.AddListener(ExitSettings);
    }

    // ���������� �������� ������� � ��������� UI.
    void OnDisable()
    {
        resolutionDropdown.onValueChanged.RemoveAllListeners();
        fullScreenToggle.onValueChanged.RemoveAllListeners();
        saveButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }
}
