using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform; 
    Vector3 Offset;
    float xAngle;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position;
    }

    private void Start()
    {
        xAngle = transform.eulerAngles.x;
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Z))
        {
            transform.eulerAngles = new Vector3(90, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(xAngle, 0, 0);
        }
    }

    void LateUpdate() 
    {
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position = playerTransform.position + Offset + new Vector3(0, 20, 10);
        }
        else
        {
            transform.position = playerTransform.position + Offset;
        }
    }
}