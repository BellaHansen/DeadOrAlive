using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController controller;
    [SerializeField] LayerMask ignoreMask;
    [SerializeField] GameObject bullet;

    [SerializeField] int HP;
    [SerializeField] int origSpeed;
    [SerializeField] int sprintMod;
    [SerializeField] int jumpsMax;
    [SerializeField] int jumpSpeed;
    [SerializeField] int gravity;
    [SerializeField] int shootDamage;
    [SerializeField] int shootDist;

    [SerializeField] float shootRate;
    [SerializeField] float regenMod;

    Vector3 moveDir;
    Vector3 playerVel;

    public bool leaningRight;
    public bool leaningLeft;
    bool isleaning;
    bool isShooting;
    bool isRegen;

    int jumpCount;
    int HPOrig;
    int speed;

    // Start is called before the first frame update
    void Start()
    {
        HPOrig = HP;
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
        HP = HPOrig;
        UpdatePlayerUI();
        controller.enabled = false;
        gameManager.instance.playerSpawnPos.transform.position;
        controller.enabled = true;
    }
    void movement()
    {
        speed = origSpeed / 2;
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

    public void HealPlayer(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HP = HPOrig;
        }
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        UpdatePlayerUI();
        StartCoroutine(FlashDamage());
        if (HP <= 0)
        {
            gameManager.instance.youLose();
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
        if(IsLeaning())
        {
            controller.Move(moveDir * speed * Time.deltaTime);
        }
        else
        {
            controller.Move(moveDir * origSpeed * Time.deltaTime);
        }

    }
    public bool IsLeaning()
    {
        if (!isleaning)
        { 
            return false;
        }

        if (Input.GetButtonUp("leanL"))
        {
            leaningLeft = !leaningLeft;
            isleaning = true;
        }

        if (Input.GetButtonUp("leanR"))
        {
            leaningRight = !leaningRight;
            isleaning = true;
        }

        return isleaning;
    }
    public void UpdatePlayerUI()
    {
        gameManager.instance.playerHPBar.fillAmount = (float)HP / HPOrig;
    }
}