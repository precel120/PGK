using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public AudioMixer audio;
    public Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    private void Start()
    {
        optionsMenu.SetActive(false);

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolution = 0;

        string previousOption;

        for(int i = 0; i < resolutions.Length; i++)
        {
            if (i == 0)
            {
                options.Add(resolutions[i].width + " x " + resolutions[i].height);
            }
            else
            {
                previousOption = resolutions[i - 1].width + " x " + resolutions[i - 1].height;
                string option = resolutions[i].width + " x " + resolutions[i].height;

                if (previousOption != option)
                {
                    options.Add(option);
                }
            }

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolution = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Menu()
    {
        optionsMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audio.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityID)
    {
        QualitySettings.SetQualityLevel(qualityID);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionID)
    {
        Resolution resolution = resolutions[resolutionID];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void quitOptions()
    {
        optionsMenu.SetActive(false);
    }
}
