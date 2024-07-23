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
        //Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //Call stateUnpause()
        gameManager.instance.stateUnpause();
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
        //gameManager.instance.playerScript.spawnPlayer();
        gameManager.instance.stateUnpause();
    }
}
