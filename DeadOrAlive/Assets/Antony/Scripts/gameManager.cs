using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add using UnityEngine.UI
using UnityEngine.UI;

//Add using TMPro
using TMPro;

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

    //Variable for player HP bar
    [SerializeField] Image playerHPBar;

    //Variable for damage flahs screen
    public GameObject damageFlashScreen;

    //Variable for the text zombie count
    [SerializeField] TMP_Text zombieCountText;

    //Variable for wave count text
    [SerializeField] TMP_Text waveCountText;

    //Variable for player
    public GameObject player;

    //Variable for player script
    public PlayerController playerScript;

    //bool to check if the game is paused
    public bool isPaused;

    //Variable for zombie count
    int zombieCount;

    //Variable for wave count
    int waveCount;



    // Start is called before the first frame update

    //Change start() to awake()
    void Awake()
    {
        //Define instance
        instance = this;

        //Define player
        player = GameObject.FindWithTag("Player");

        //Define player script
        playerScript = player.GetComponent<PlayerController>();
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
        //Toggle isPaused bool
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
        //Toggle the isPaused bool
        isPaused = !isPaused;

        Time.timeScale = 1;

        //Turn off the cursor
        Cursor.visible = false;

        //Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        //Shut down the menu
        menuActive.SetActive(isPaused);

        //Set menu active to null
        menuActive = null;
    }

    //Create a function to update the game goal
    public void updateGameGoal(int amount)
    {
        zombieCount += amount;

        waveCount += amount;

        zombieCountText.text = zombieCount.ToString("F0");

        waveCountText.text = waveCount.ToString("F0");
            

        if(zombieCount <= 0)
        {
            //Player wins
            //pause the game
            statePause();

            //Set menu active to menu win
            menuActive = menuWin;

            //Toggle menu active
            menuActive.SetActive(isPaused);
        }
    } 

    //Create a function to lose 
    public void youLose()
    {
        //Pause the game 
        statePause();

        //Set the menu active to menu lose
        menuActive = menuLose;

        //Toggle menu active
        menuActive.SetActive(true);
    }
}
