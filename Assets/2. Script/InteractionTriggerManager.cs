using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionTriggerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody charRigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Trigger"))
        {
            GameObject[] gameObjects = other.GetComponent<Trigger_ObjArr>().GameObjects;
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Trigger"))
        {
            GameObject[] gameObjects = other.GetComponent<Trigger_ObjArr>().GameObjects;
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].SetActive(false);
            }
        }
    }
}
