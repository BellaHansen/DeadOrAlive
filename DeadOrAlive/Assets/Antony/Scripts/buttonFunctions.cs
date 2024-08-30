using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add unity engine scene management to load scenes
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class buttonFunctions : MonoBehaviour
{
    public AudioMixer aud;

    Resolution[] resolutions;
    
    List<string> resOption = new List<string>();
    //keep in game manager
    [SerializeField] Dropdown resolutionDrop; 
    public void setVolume(float vol)
    {
        aud.SetFloat("volume", vol);
    }
    public void setQuality(int qualIndex)
    {
        QualitySettings.SetQualityLevel(qualIndex);
    }
    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDrop.ClearOptions();
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resOption.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                i = currentResIndex;
            }
        }
        resolutionDrop.AddOptions(resOption);
        resolutionDrop.value = currentResIndex;
        resolutionDrop.RefreshShownValue();
    }
    public void SetResolution(int curResIndex)
    {
        Resolution res = resolutions[curResIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
    //Create resume function
    public void resume()
    {
        //Call the stateUnpause() from game manager
        gameManager.instance.stateUnpause();
    }

    //Create restart function
    public void restart()
    {
        //load the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //access to game manager and call state unpause because if I don't do this the time scale will be zero
        gameManager.instance.stateUnpause();

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Exit the game
    public void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    //Create respawn function
    public void respawn()
    {
        gameManager.instance.playerScript.SpawnPlayer();
        gameManager.instance.stateUnpause();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("mainmenu");
    }
    public void GameOptions()
    {
        gameManager.instance.camera.sensitivty = 
    }
}
