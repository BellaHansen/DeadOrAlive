using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add unity engine scene management to load scenes
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
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
}
