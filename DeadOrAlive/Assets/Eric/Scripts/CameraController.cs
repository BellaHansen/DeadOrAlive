using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class CameraController : MonoBehaviour
{
    //camera values for controls
    [SerializeField] int sensitivty;
    [SerializeField] int lockvertmin, lockvertmax;
    [SerializeField] bool invertY;

    GameObject player;
    bool leanleft;
    bool leanright;
    public float rotX;

    // Start is called before the first frame update
    void Start()
    {
        //removal of cursor code
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        float mouseY = Input.GetAxis("Mouse Y") * sensitivty * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * sensitivty * Time.deltaTime;

        if (invertY)
            rotX += mouseY;
        else
            rotX -= mouseY;

        //clamp rotX on the X-axis
        rotX = Mathf.Clamp(rotX, lockvertmin, lockvertmax);

        //rotate the cam on the X-axis
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);

        //rotate the player on the Y-Axis
        transform.parent.Rotate(Vector3.up * mouseX);

        LeanCamera();

    }

    void LeanCamera()
    {
        if (gameManager.instance.playerScript.IsLeaning() && gameManager.instance.playerScript.currentLeanState == PlayerController.LeanState.Right)
        {
            transform.localRotation = Quaternion.Euler(rotX, Input.GetAxis("Mouse Y"), transform.rotation.z - 45);
        }
        else if (gameManager.instance.playerScript.IsLeaning() && gameManager.instance.playerScript.currentLeanState == PlayerController.LeanState.Left)
        {
            transform.localRotation = Quaternion.Euler(rotX, Input.GetAxis("Mouse Y"), transform.rotation.z + 45);
        }
    }
}
