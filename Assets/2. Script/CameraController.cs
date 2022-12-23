using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform; //Player의 transform 정보를 담을 변수 선언 
    Vector3 Offset;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position;
    }

    void LateUpdate() //카메라 움직임은 주로 LateUpdate에 적습니다.
    {
        transform.position = playerTransform.position + Offset; 
    }
}