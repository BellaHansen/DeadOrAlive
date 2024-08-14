using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    //Variable for flash 
    [SerializeField] Renderer model;


    //Ontriggerenter for the check point
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance != null && gameManager.instance.playerSpawnPos.transform.position != this.transform.position)
        {
            gameManager.instance.playerSpawnPos.transform.position = transform.position;
            StartCoroutine(flashModel());
        }
    }

    //Flash the checkpoint
    IEnumerator flashModel()
    {
        model.material.color = Color.green;

        gameManager.instance.checkpointPopup.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        gameManager.instance.checkpointPopup.SetActive(false);

        model.material.color = Color.white;
    }
}
