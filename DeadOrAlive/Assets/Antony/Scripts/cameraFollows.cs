using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollows : MonoBehaviour
{
    //Variable for camera sensitivity
    [SerializeField] int sensitivity;

    //Variables to set vertical minimun and maximum
    [SerializeField] int lockVertMin;
    [SerializeField] int lockVertMax;

    //Variable to check if the mouse is inverted
    [SerializeField] bool invertY;

    //Variable for rotation value 
    float rotX;

    // Start is called before the first frame update
    void Start()
    {
        //turn the cursor off
        Cursor.visible = false;

        //Lock the cursor so it does not go outside the window
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the input
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        //Check if the mouse is inverted
        if (invertY)
            //Add where the mouse y is coming
            rotX += mouseY;
        else 
            //Otherwise, substract it
            rotX -= mouseY;

        //Clamp rotX of the x-axis
        rotX = Mathf.Clamp(rotX, lockVertMin, lockVertMax);

        //Rotate camera on the x-axis
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);

        //Rotate player on the y-axis
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
