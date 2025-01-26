using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Класс SettingsManager, наследующий MonoBehaviour, управляет настройками игры.
public class SettingsManager : MonoBehaviour
{
    // Выпадающий список для выбора разрешения экрана.
    public Dropdown resolutionDropdown;

    // Переключатель для выбора полноэкранного режима.
    public Toggle fullScreenToggle;

    // Кнопка для сохранения настроек.
    public Button saveButton;

    // Кнопка для выхода из настроек.
    public Button exitButton;

    // Массив доступных разрешений экрана.
    Resolution[] resolutions;

    // Метод Awake вызывается при инициализации объекта.
    void Awake()
    {
        // Инициализируем доступные разрешения и загружаем сохраненные настройки.
        InitializeResolutions();
        LoadSettings();
    }

    // Метод для инициализации доступных разрешений.
    void InitializeResolutions()
    {
        // Очищаем список опций перед добавлением новых.
        resolutionDropdown.ClearOptions();

        // Создаем список строк для опций разрешений.
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        // Заполняем список опций разрешений.
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate.ToString();
            options.Add(option);

            // Определяем текущее разрешение.
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].refreshRate.Equals(Screen.currentResolution.refreshRate))
            {
                currentResolutionIndex = i;
            }
        }

        // Добавляем опции в выпадающий список и устанавливаем текущее разрешение.
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // Метод для установки разрешения экрана.
    public void SetResolution(int resolutionIndex)
    {
        // Запускаем корутину для установки разрешения.
        StartCoroutine(SetResolutionCoroutine(resolutionIndex));
    }

    // Корутина для установки разрешения экрана.
    IEnumerator SetResolutionCoroutine(int resolutionIndex)
    {
        // Добавляем небольшую задержку перед изменением разрешения.
        yield return new WaitForEndOfFrame();

        // Устанавливаем новое разрешение.
        Resolution resolution = resolutions[resolutionIndex];
        FullScreenMode fullScreenMode = fullScreenToggle.isOn ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
        Screen.SetResolution(resolution.width, resolution.height, fullScreenMode, resolution.refreshRate);
    }

    // Метод для установки полноэкранного режима.
    public void SetFullScreen(bool isFullScreen)
    {
        FullScreenMode fullScreenMode = isFullScreen ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
        Screen.fullScreenMode = fullScreenMode;
    }

    // Метод для сохранения настроек.
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
        PlayerPrefs.SetInt("FullScreenPreference", System.Convert.ToInt32(Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen));
        PlayerPrefs.Save();
    }

    // Метод для загрузки сохраненных настроек.
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

    // Метод для выхода из меню настроек.
    public void ExitSettings()
    {
        // Логика для выхода из меню настроек.
        gameObject.SetActive(false);
        MainMenuCanvas.Instance.Logo.SetActive(true);
        MainMenuCanvas.Instance.PlayButton.SetActive(true);
        MainMenuCanvas.Instance.SettingsButton.SetActive(true);
        MainMenuCanvas.Instance.ExitButtonMenu.SetActive(true);
    }

    // Привязка методов к элементам UI.
    void OnEnable()
    {
        resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(resolutionDropdown.value); });
        fullScreenToggle.onValueChanged.AddListener(SetFullScreen);
        saveButton.onClick.AddListener(SaveSettings);
        exitButton.onClick.AddListener(ExitSettings);
    }

    // Отключение привязок методов к элементам UI.
    void OnDisable()
    {
        resolutionDropdown.onValueChanged.RemoveAllListeners();
        fullScreenToggle.onValueChanged.RemoveAllListeners();
        saveButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }
}
