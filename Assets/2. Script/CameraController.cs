using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform; //Player�� transform ������ ���� ���� ���� 
    Vector3 Offset;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position;
    }

    void LateUpdate() //ī�޶� �������� �ַ� LateUpdate�� �����ϴ�.
    {
        transform.position = playerTransform.position + Offset; 
    }
}