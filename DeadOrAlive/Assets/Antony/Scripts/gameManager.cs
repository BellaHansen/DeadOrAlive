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
    [SerializeField]  GameObject menuWin;

    //Variable for lose menu
    [SerializeField] GameObject menuLose;

    [SerializeField] GameObject inventoryMenu;


    //Variable for player HP bar
    public Image playerHPBar;

    //Variable for damage flahs screen
    public GameObject damageFlashScreen;

    //Variable for the text zombie count
    [SerializeField] TMP_Text zombieCountText;

    //Variable for wave count text
    [SerializeField] TMP_Text waveCountText;

    //Variable for ammo current and max
    public TMP_Text ammoCur, ammoMax;

    //Variable for player
    public GameObject player;

    //Variable for player script
    public PlayerController playerScript;

    //Variable for the player spawn position
    public GameObject playerSpawnPos;

    //Variable for checkpoint 
    public GameObject checkpointPopup;

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

        //Define the player spawn position
        playerSpawnPos = GameObject.FindWithTag("Player Spawn Pos");

        // Deactivate win menu
        menuWin.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //This will turn the menu on and off 
        if (Input.GetButtonDown("Cancel"))
        {
            //isPaused = !isPaused;
            if (menuActive == null)
            {
                //Call statePause()
                statePause();

                //Set menu active to menu pause
                menuActive = menuPause;

                //Turn on the menu active
                menuActive.SetActive(isPaused);
            }
            else if (menuActive == menuPause)
            {
                stateUnpause();
            }
        }
       
    }

    //Function to pause
    public void statePause()
    {
         isPaused = !isPaused;

        //Set time scale to zero
        Time.timeScale = 0;

        //Make the cursor visible to be able to click 
        Cursor.visible = true;

        //Optional: Confine the cursor to that window
        Cursor.lockState = CursorLockMode.Confined;
    }

    //Function to unpause the game
    public void stateUnpause()
    {
        isPaused = !isPaused;

        //Set the time scale to 1
        Time.timeScale = 1;

        //Turn the cursor off
        Cursor.visible = false;

        //Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        //Shut down the menu
        menuActive.SetActive(isPaused);

        //Set the active menu to null to empty it out
        menuActive = null;
    }

        public void updateGameGoal(int amount)
        {
         
            // Update zombie count
            zombieCount += amount;

                // Update zombie count display
            zombieCountText.text = zombieCount.ToString("F0");
           
                // Update wave count display
            waveCountText.text = waveManager.instance.currentWave.ToString("F0");

            // Check win condition
            if (zombieCount <= 0 && waveManager.instance.currentWave >= waveManager.instance.spawners.Length)
            {
                // Player wins
                // Pause the game
                statePause();
               
                menuActive = menuWin;
             
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


