using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoorOpen : MonoBehaviour
{
    [SerializeField] private bool isOpen = false;

    [SerializeField] private bool closed = false;
    [SerializeField] private Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isOpen)
            {
                anim.Play("DoorOpen", 0, 0.0f);
            }
            else if (closed)
            {
                anim.Play("DoorClose", 0, 0.0f);
            }
        }
    }
}
