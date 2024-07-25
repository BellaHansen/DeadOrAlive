using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add using UnityEngine.UI
using UnityEngine.UI;

//Add using TMPro
using TMPro;
using static UnityEditor.Progress;

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

    //inventorymanager 
    [SerializeField] GameObject inventoryManager;
    [SerializeField] private List<Image> itemIcon;
    [SerializeField] private List<TMP_Text> itemQuantities;
    [SerializeField] private Image currentItemIcon;
    [SerializeField] private TMP_Text currentItemQuantity;
    [SerializeField] private TMP_Text ammoDisplay;
    [SerializeField] private List<inventoryItem> items = new List<inventoryItem>();
    public int maxItems = 4;
    private int currentItemIndex = 0;

    //Variable for player HP bar
    public Image playerHPBar;

    //Variable for damage flahs screen
    public GameObject damageFlashScreen;

    //Variable for the text zombie count
    [SerializeField] TMP_Text zombieCountText;

    //Variable for wave count text
    [SerializeField] TMP_Text waveCountText;

    //Variable for ammo current and max
    [SerializeField] TMP_Text ammoCur, ammoMax;

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

    // references playerState
    private playerState playerHealth;

    private WeaponController weaponController;
    private int currentAmmo;
    private int maxAmmo;

    private bool isVisible;



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

        weaponController = player.GetComponent<WeaponController>();
        currentAmmo = weaponController.getCurrentAmmo();
        maxAmmo = weaponController.getMaxAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        PauseInput();
        InventoryInput();
    }
    private void PauseInput()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuActive == null)
            {
                statePause();
                menuActive = menuPause;
                menuActive.SetActive(true);
            }
            else if (menuActive == menuPause)
            {
                stateUnpause();
            }
        }
    }

    private void InventoryInput()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            isVisible = !isVisible;
            inventoryManager.SetActive(isVisible);
            UpdateInventoryUI();
        }

        if (isVisible)
        {
            if (Input.GetButtonDown("NextItem"))
            {
                NextItem();
            }
            if (Input.GetButtonDown("PreviousItem"))
            {
                PreviousItem();
            }
        }
    }
    public void ToggleInventoryUI()
    {
        isVisible = !isVisible;
        inventoryManager.SetActive(isVisible);
        UpdateInventoryUI();
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

        //update zombie count
        /*
        zombieCount += amount;

        //update zombie count display
        zombieCountText.text = zombieCount.ToString("F0");

        //update wave count display
        waveCountText.text = waveManager.instance.currentWave.ToString("F0");


        if (zombieCount <= 0 && waveManager.instance.currentWave >= waveManager.instance.spawners.Length)
        {
            //Player wins
            //pause the game
            statePause();

            //Set menu active to menu win
            menuActive = menuWin;

            //Toggle menu active
            menuActive.SetActive(isPaused);
        } */
        public void updateGameGoal(int amount)
        {
            // Check if waveManager instance is null
            if (waveManager.instance == null)
            {
                Debug.LogError("waveManager instance is null");
                return;
            }

            // Update zombie count
            zombieCount += amount;

            // Check if zombieCountText is null
            if (zombieCountText != null)
            {
                // Update zombie count display
                zombieCountText.text = zombieCount.ToString("F0");
            }
            else
            {
                Debug.LogError("zombieCountText is not assigned");
            }

            // Check if waveCountText is null
            if (waveCountText != null)
            {
                // Update wave count display
                waveCountText.text = waveManager.instance.currentWave.ToString("F0");
            }
            else
            {
                Debug.LogError("waveCountText is not assigned");
            }

            // Check win condition
            if (zombieCount <= 0 && waveManager.instance.currentWave >= waveManager.instance.spawners.Length)
            {
                // Player wins
                // Pause the game
                statePause();

                // Set menu active to menu win
                if (menuWin != null)
                {
                    menuActive = menuWin;
                }
                else
                {
                    Debug.LogError("menuWin is not assigned");
                }

                // Toggle menu active
                if (menuActive != null)
                {
                    menuActive.SetActive(isPaused);
                }
                else
                {
                    Debug.LogError("menuActive is not assigned");
                }
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
    public void equipWeapon(Weapons weapon)
    {
        //euip player with picked up weapon
        weaponController.equipWeapon(weapon);
        updateAmmoUI();
    }

    public void updateAmmoUI()
    {
        ammoCur.text = weaponController.getCurrentAmmo().ToString();
        ammoMax.text = weaponController.getMaxAmmo().ToString();
    }

    public void healPlayer(int healthAmount)
    {
        playerHealth.Heal(healthAmount);
        updateHealthUI();
    }

    public void playerTakeDamage(int damageAmount)
    {
        playerHealth.TakeDamage(damageAmount);
        updateHealthUI();
    }

    public void updateHealthUI()
    {
        playerHPBar.fillAmount = (float)playerHealth.GetCurrentHealth() / playerHealth.GetMaxHealth();
    }
    public void UpdateInventoryUI()
    {
        for (int i = 0; i < itemIcon.Count; i++)
        {
            if (i < items.Count)
            {
                itemIcon[i].sprite= items[i].itemIcon;
                itemQuantities[i].text = items[i].itemNum.ToString();
            }
            else
            {
                itemIcon[i].sprite = null;
                itemQuantities[i].text = "0";
            }
        }

        inventoryItem currentItem = GetCurrentItem();
        if (currentItem != null)
        {
            currentItemIcon.sprite = currentItem.itemIcon;
            currentItemQuantity.text = currentItem.itemNum.ToString();
        }
        else
        {
            currentItemIcon.sprite = null;
            currentItemQuantity.text = "0";
        }
    }
    public bool AddItem(inventoryItem item)
    {
        if (items.Count >= maxItems)
        {
            Debug.Log("Inventory is full");
            return false;
        }
        items.Add(item);
        UpdateInventoryUI();  // Updates the UI
        return true;
    }

    public void AddAmmo(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
        updateAmmoUI();
    }

    public bool RemoveItem(inventoryItem item)
    {
        bool result = items.Remove(item);
        UpdateInventoryUI();  // Update the UI through the GameManager
        return result;
    }

    public inventoryItem GetCurrentItem()
    {
        if (items.Count == 0) return null;
        return items[currentItemIndex];
    }

    public void NextItem()
    {
        if (items.Count == 0) return;
        currentItemIndex = (currentItemIndex + 1) % items.Count;
        UpdateInventoryUI();  // Update the UI through the GameManager
    }

    public void PreviousItem()
    {
        if (items.Count == 0) return;
        currentItemIndex = (currentItemIndex - 1 + items.Count) % items.Count;
        UpdateInventoryUI();
    }

}



