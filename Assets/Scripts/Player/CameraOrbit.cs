using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{

    public float turnSpeed = 4.0f;
    public float yAngle = 5.0f;
    public float zAngle = 7.0f;
    public Transform player;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0, yAngle, zAngle);
    }

    void LateUpdate()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
