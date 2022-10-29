using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private float horizontal;
    private float vertical;

    private CharacterController controller;

    public GameObject[] jackOLanterns;
    private Lantern nearbyLantern;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        jackOLanterns = GameObject.FindGameObjectsWithTag("JackOLantern");
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && nearbyLantern != null)
        {
            nearbyLantern.LightUp();
            if (CompletenessCheck())
            {
                //Display win banner in UI
                Debug.Log("You win!");
            }
        }

        //Keep track of how many lanterns are lit
    }

    private void FixedUpdate()
    {
        //camera forward and right vectors:
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 DesiredMoveDirection = forward * vertical + right * horizontal;

        controller.Move(DesiredMoveDirection * speed);
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

    private bool CompletenessCheck()
    {
        foreach(GameObject lantern in jackOLanterns)
        {
            if (!lantern.GetComponent<Lantern>().lit)
            {
                return false;
            }
        }

        return true;
    }
}
