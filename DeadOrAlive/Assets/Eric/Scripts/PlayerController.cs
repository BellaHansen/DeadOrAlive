using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEditor;

using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController controller;
    [SerializeField] LayerMask ignoreMask;
    [SerializeField] GameObject bullet;
    [SerializeField] List<weaponStats> weapons = new List<weaponStats>();
    [SerializeField] GameObject weaponModel;
    [SerializeField] AudioSource aud;


    int selectedGun;


    [SerializeField] Animator anim;


    [SerializeField] int origSpeed;
    [SerializeField] int sprintMod;
    [SerializeField] int jumpsMax;
    [SerializeField] int jumpSpeed;
    [SerializeField] int gravity;
    [SerializeField] int shootDamage;
    [SerializeField] int shootDist;
    [SerializeField] int animSpeedTrans;
    [SerializeField] float audMoveVol;
    [SerializeField] AudioClip[] audMove;
    [SerializeField] AudioClip[] audHurt;
    [SerializeField] float audHurtVol;
    [SerializeField] AudioClip[] audJump;
    [SerializeField] float audJumpVol;


    [SerializeField] float shootRate;
    [SerializeField] int HP;


    Vector3 moveDir;
    Vector3 playerVel;
    

    public bool isShooting;
    public bool isBeingDamaged;


    public int jumpCount;
    public int HPOrig;
    bool isMoving;
    bool isCrouched;

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

        //If the game is not paused
        if (!gameManager.instance.isPaused)
        {
                movement();
        }
        sprint();

    }

    public void SpawnPlayer()
    {
        HP = HPOrig;
        UpdatePlayerUI();
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawnPos.transform.position;
        controller.enabled = true;
    }
    void movement()
    {
        if (controller.isGrounded)
        {
            jumpCount = 0;  // reset jumps to jump again
            playerVel = Vector3.zero;   // gravity y velocity stops decreasing
        }

        moveDir = Input.GetAxis("Vertical") * transform.forward +
                  Input.GetAxis("Horizontal") * transform.right;

        controller.Move(moveDir * origSpeed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && jumpCount < jumpsMax)
        {
            jumpCount++;
            playerVel.y = jumpSpeed;
            aud.PlayOneShot(audJump[Random.Range(0, audJump.Length)], audJumpVol);
        }

        controller.Move(playerVel * Time.deltaTime);
        playerVel.y -= gravity * Time.deltaTime; // based on time not on frames

        //shoot mechanic
        if (Input.GetButton("Fire1") && !isShooting)
        {
            StartCoroutine(shoot());
        }
        PlayerCrouch();
        if (isCrouched)
        {
            anim.SetBool("Crouching", true);
        }
        else
        {
            anim.SetBool("Crouching", false);
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            AimPlayer();
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

        anim.SetTrigger("Shoot");

        weapons[selectedGun].ammoCur--;

        aud.PlayOneShot(weapons[selectedGun].shootSound[Random.Range(0, weapons[selectedGun].shootSound.Length)], weapons[selectedGun].shootVol);

        UpdatePlayerUI();

        RaycastHit hit;     // origin: camera position, dir: where cam is looking, out: to know what we hit, distance
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootDist, ~ignoreMask))
        {
            Debug.Log(hit.collider.name); // will return what gets hit

            IDamage dmg = hit.collider.GetComponent<IDamage>();

            if (hit.transform != transform)
            {
                dmg.TakeDamage(shootDamage);
                Instantiate(bullet, hit.point, transform.rotation);
            }
            else
            {
                Instantiate(weaponModel, hit.point, Quaternion.identity);
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
        aud.PlayOneShot(audHurt[Random.Range(0, audHurt.Length)], audHurtVol);

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
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.damageFlashScreen.SetActive(false);
    }

    void PlayerCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isCrouched)
            {
                anim.SetTrigger("Crouch");
                isCrouched = true;
            }
            else
            {
                anim.SetTrigger("Uncrouch");
                isCrouched = false;
            }
        }

    }

    void AimPlayer()
    {
        anim.SetBool("aim", true);
    }


    public void UpdatePlayerUI()
    {
        gameManager.instance.playerHPBar.fillAmount = (float)HP / HPOrig;
        if (weapons.Count > 0)
        {
            
            gameManager.instance.ammoCur.text = weapons[selectedGun].ammoCur.ToString("F0");
            gameManager.instance.ammoMax.text = weapons[selectedGun].ammoMax.ToString("F0");
        }

    }
    
    public void GetGunStats(weaponStats gun)

    {
        weapons.Add(gun);
        selectedGun = weapons.Count - 1;

        UpdatePlayerUI();

        shootDamage = gun.itemDamage;
        shootDist = gun.itemDistance;
        shootRate = gun.shootRate;

        weaponModel.GetComponent<MeshFilter>().sharedMesh = gun.itemModel.GetComponent<MeshFilter>().sharedMesh;
        weaponModel.GetComponent<MeshRenderer>().sharedMaterial = gun.itemModel.GetComponent<MeshRenderer>().sharedMaterial;

    }

    void SelectGun()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedGun < weapons.Count - 1)
        {
            ++selectedGun;
            ChangeGun();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedGun > 0)
        {
            --selectedGun;
            ChangeGun();
        }



    }

    void ChangeGun()
    {
        UpdatePlayerUI();

        shootDamage = weapons[selectedGun].itemDamage;
        shootDist = weapons[selectedGun].itemDistance;
        shootRate= weapons[selectedGun].shootRate;

        weaponModel.GetComponent<MeshFilter>().sharedMesh = weapons[selectedGun].GetComponent<MeshFilter>().sharedMesh;
        weaponModel.GetComponent<MeshRenderer>().sharedMaterial = weapons[selectedGun].GetComponent<MeshRenderer>().sharedMaterial;

    }

}