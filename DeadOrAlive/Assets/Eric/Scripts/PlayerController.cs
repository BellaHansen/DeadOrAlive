using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController controller;
    [SerializeField] LayerMask ignoreMask;
    [SerializeField] GameObject bullet;

    [SerializeField] int origSpeed;
    [SerializeField] int sprintMod;
    [SerializeField] int jumpsMax;
    [SerializeField] int jumpSpeed;
    [SerializeField] int gravity;
    [SerializeField] int shootDamage;
    [SerializeField] int shootDist;

    [SerializeField] float shootRate;
    [SerializeField] float HP;
    [SerializeField] float regenMod;
    
    Vector3 moveDir;
    Vector3 playerVel;
    public enum LeanState { None, Left, Right };
    public LeanState currentLeanState;

    public List<IInventoryItem> inventoryItems;

   public  bool isleaning;
    public bool isShooting;
   public  bool isRegen;
    public bool isBeingDamaged;

    public int jumpCount;
    public int HPOrig;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        HPOrig = (int)HP;
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootDist, Color.green);

        movement();
        sprint();
       

    }

    public void SpawnPlayer()
    {
        currentLeanState = LeanState.None;
        HP = HPOrig;
        UpdatePlayerUI();
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawnPos.transform.position;
        controller.enabled = true;
    }
    void movement()
    {
        speed = origSpeed;
        if (controller.isGrounded)
        {
            jumpCount = 0;  // reset jumps to jump again
            playerVel = Vector3.zero;   // gravity y velocity stops decreasing
        }

        moveDir = Input.GetAxis("Vertical") * transform.forward +
                  Input.GetAxis("Horizontal") * transform.right;


        if (Input.GetButtonDown("Jump") && jumpCount < jumpsMax)
        {
            jumpCount++;
            playerVel.y = jumpSpeed;
        }

        controller.Move(playerVel * Time.deltaTime);
        playerVel.y -= gravity * Time.deltaTime; // based on time not on frames

        //shoot mechanic
        if (Input.GetButton("Fire1") && !isShooting)
        {
            StartCoroutine(shoot());
        }
        LeanMechanics();
        if (!isBeingDamaged && HP <= HPOrig)
        {
            StartCoroutine(Regen());
        }
    }

    void sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            origSpeed *= sprintMod;
            
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            origSpeed /= sprintMod;
        }
    }

    IEnumerator shoot()
    {
        isShooting = true;
       

        RaycastHit hit;     // origin: camera position, dir: where cam is looking, out: to know what we hit, distance
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootDist, ~ignoreMask))
        {
            Debug.Log(hit.collider.name); // will return what gets hit

            IDamage dmg = hit.collider.GetComponent<IDamage>();

            if (hit.transform != transform && dmg != null)
            {
                dmg.TakeDamage(shootDamage);
                //Instantiate(cube, hit.point, transform.rotation);
            }
        }

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    public void HealPlayer(int healthAmount)
    {
        HP += healthAmount;
        UpdatePlayerUI();
    }

    public void TakeDamage(int amount)
    {
        isBeingDamaged = true;
        HP -= amount;
        UpdatePlayerUI();
        StartCoroutine(FlashDamage());
        if (HP <= 0)
        {
            gameManager.instance.youLose();
        }
        else
        {
            isBeingDamaged = false;
        }
        
    }

    IEnumerator FlashDamage()
    {
        gameManager.instance.damageFlashScreen.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        gameManager.instance.damageFlashScreen.SetActive(false);
    }

    void LeanMechanics()
    {
        if (isleaning)
        {
            controller.Move(moveDir * (speed / 2) * Time.deltaTime);
        }
        else
        {
            controller.Move(moveDir * speed * Time.deltaTime);
        }

    }
    public bool IsLeaning()
    {
        if (currentLeanState == LeanState.None)
        {
            if (Input.GetButtonDown("leanL"))
            {
                currentLeanState = LeanState.Left;
                isleaning = true;
            }
            else if (Input.GetButtonDown("leanR"))
            {
                currentLeanState = LeanState.Right;
                isleaning = true;
            }
        }
        else if (currentLeanState == LeanState.Left)
        {
            if (Input.GetButtonDown("leanL"))
            {
                currentLeanState = LeanState.None;
                isleaning = false;
            }
            else if (Input.GetButtonDown("leanR"))
            {
                currentLeanState = LeanState.Right;
            }
        }
        else if (currentLeanState == LeanState.Right)
        {
            if (Input.GetButtonDown("leanR"))
            {
                currentLeanState = LeanState.None;
                isleaning = false;
            }
            else if (Input.GetButtonDown("leanL"))
            {
                currentLeanState = LeanState.Left;
            }
        }
        return isleaning;
    }


    public void UpdatePlayerUI()
    {
        gameManager.instance.playerHPBar.fillAmount = HP / HPOrig;
    }

    IEnumerator Regen()
    {
        HP += regenMod;
        yield return new WaitForSeconds(4f);
    }
}