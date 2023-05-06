using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
    [SerializeField]
    Camera theCamera;
    [SerializeField]
    UIManager uiManager;
    [SerializeField]
    PlayerController playerController;


    private void OnTriggerStay(Collider other) 
    {
        if (other.tag == "Player")
        {
            if (Input.GetKey(playerController.interactionKey))
            {
                OpenShop();
            }
        }
    }

    public void OpenShop()
    {
        
    }
}
