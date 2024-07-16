using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamage
{
    [SerializeField] CharacterController controller;
    [SerializeField] LayerMask ignoreMask;
    [SerializeField] GameObject bullet;

    [SerializeField] int HP;
    [SerializeField] int speed;
    [SerializeField] int sprintMod;
    [SerializeField] int jumpsMax;
    [SerializeField] int jumpSpeed;
    [SerializeField] int gravity;


    [SerializeField] int shootDamage;
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] float regenMod;


    Vector3 moveDir;
    Vector3 playerVel;

    enum LeanState { None, Left, Right }
    LeanState CurrentLeanstate;

    int jumpCount;
    int HPOrig;

    bool isShooting;

    bool isRegen;
    // Start is called before the first frame update
    void Start()
    {
        HPOrig = HP;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootDist, Color.green);

        movement();
        sprint();

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
        controller.Move(moveDir * speed * Time.deltaTime); // time based not frame based

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
            speed *= sprintMod;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            speed /= sprintMod;
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

    void HealPlayer(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HP = HPOrig;
        }
    }

    public void TakeDamage(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            gameManager.instance.youLose();
        }
    }

    void LeanMechanics()
    {
        int tempspeed = speed;
        IsLeaning();
        if (CurrentLeanstate == LeanState.Right)
        {
            Camera.main.transform.localRotation = Quaternion.Euler(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), Camera.main.transform.rotation.z - 45);
        }
        else if (CurrentLeanstate == LeanState.Left)
        {
            Camera.main.transform.localRotation = Quaternion.Euler(Camera.main.transform.rotation.x, 0, Camera.main.transform.rotation.z + 45);
        }
    }
   public void IsLeaning()
    {
        if (CurrentLeanstate == LeanState.None)
        {
            if (Input.GetButtonUp("leanR"))
            {
                CurrentLeanstate = LeanState.Right;
            }
            else if(Input.GetButtonUp("leanL"))
            {
                CurrentLeanstate = LeanState.Left;
            }
        }
        else if (CurrentLeanstate == LeanState.Right)
        {
            if (Input.GetButtonUp("leanR"))
            {
                CurrentLeanstate = LeanState.None;
            }
            else if (Input.GetButtonUp("leanL"))
            {
                CurrentLeanstate = LeanState.Left;
            }
        }
        else if(CurrentLeanstate == LeanState.Left)
        {
            if (Input.GetButtonUp("leanR"))
            {
                CurrentLeanstate = LeanState.Right;
            }
            else if (Input.GetButtonUp("leanL"))
            {
                CurrentLeanstate = LeanState.None;
            }
        }
    }
}