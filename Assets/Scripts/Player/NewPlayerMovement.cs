using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NewPlayerMovement : MonoBehaviour
{
    public GameObject[] jackOLanterns;
    public PumpkinManager pumpkinManager;
    private Lantern nearbyLantern;
    public TextMeshProUGUI UIText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && nearbyLantern != null)
        {
            nearbyLantern.LightUp();
            if (pumpkinManager.CompletenessCheck())
            {
                UIText.text = "You Win!";
                UIText.gameObject.SetActive(true);
                StartCoroutine("Reset");
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

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(0);
    }
}
