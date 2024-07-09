using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    //create a public static class instance
    public static gameManager instance;

    //Variable to activate the menu
    [SerializeField] GameObject menuActive;

    //Variable for menu pause
    [SerializeField] GameObject menuPause;

    //Variable for win menu
    [SerializeField] GameObject menuWin;

    //Variable for lose menu
    [SerializeField] GameObject menuLose;

    //bool to check if the game is paused
    public bool isPaused;



    // Start is called before the first frame update

    //Change start() to awake()
    void Awake()
    {
        //Define instance
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {

            //Check if the menu active is null
            if (menuActive == null)
            {
                //Call function to pause the game
                statePause();

                //Set the menu active equal to the menu pause
                menuActive = menuPause;

                //Turn on the menu active
                menuActive.SetActive(isPaused);
            }
            else if (menuActive == menuPause)
            {
                //Call stateUnpause()
                stateUnpause();
            }
        }
    }

    //Function to pause
    public void statePause()
    {
        //Taggle isPaused bool
        isPaused = !isPaused;

        //Stop everything from moving except UI
        Time.timeScale = 0;

        //Turn the cursor on
        Cursor.visible = true;

        //Confine the cursor to the window
        Cursor.lockState = CursorLockMode.Confined;
    }

    //Function to unpause the game
    public void stateUnpause()
    {
        //Taggle the isPaused bool
        isPaused = !isPaused;

        Time .timeScale = 1;

        //Turn off the cursor
        Cursor.visible = false;

        //Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        //Shut down the menu
        menuActive.SetActive(isPaused);

        //Set menu active to null
        menuActive = null;
    }
}
