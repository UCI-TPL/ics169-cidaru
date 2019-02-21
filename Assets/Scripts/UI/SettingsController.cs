using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Dropdown graphicsDropdown;

    public Slider musicSlider;
    public Slider sfxSlider;
    //public Slider masterVolumeSlider;

    private Resolution[] resolutions;

    private void Start()
    {
        // Add possible resolutions to tab and sets current one
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == 60 || resolutions[i].refreshRate == 144)
            {
                options.Add(resolutions[i].ToString());
            }

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
        // Sets fullscreen current value
        fullscreenToggle.isOn = Screen.fullScreen;

        // Sets graphics current value
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        graphicsDropdown.RefreshShownValue();

        if (!PlayerPrefs.HasKey("MusicVolume"))
            PlayerPrefs.SetFloat("MusicVolume", 0);

        if (!PlayerPrefs.HasKey("SFXVolume"))
            PlayerPrefs.SetFloat("SFXVolume", 0);

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        audioMixer.SetFloat("bgmVolume", PlayerPrefs.GetFloat("MusicVolume"));

        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("SFXVolume"));
    }

    private void Update()
    {
        /*
        // Sets master volume current value
        float volume;
        bool check = audioMixer.GetFloat("masterVolume", out volume);

        if (check)
        {
            masterVolumeSlider.value = volume;
        }
        else
        {
            masterVolumeSlider.value = 0;
        }
        */
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetGraphics(int graphicIndex)
    {
        QualitySettings.SetQualityLevel(graphicIndex);
    }

    // NOT USED RIGHT NOW
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);

        PlayerPrefs.SetFloat("masterVol", volume);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("bgmVolume", volume);

        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", volume);

        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void CloseSettingMenu()
    {
        gameObject.SetActive(false);
    }
}
