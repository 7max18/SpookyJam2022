using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NewPlayerMovement : MonoBehaviour
{
    public GameObject[] jackOLanterns;
    public PumpkinManager pumpkinManager;
    private Lantern nearbyLantern;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && nearbyLantern != null)
        {
            nearbyLantern.LightUp();
            if (pumpkinManager.CompletenessCheck())
            {
                //Display win banner in UI
                Debug.Log("You win!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Keep track of how many lanterns are lit
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JackOLantern"))
        {
            nearbyLantern = other.gameObject.GetComponent<Lantern>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JackOLantern"))
        {
            nearbyLantern = null;
        }
    }
}
