using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell_Interaction : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    UIManager uiManager;
    private void Start() {
        //playerController = Object.FindObjectOfType<PlayerController>();
        //uiManager = Object.FindObjectOfType<UIManager>();
    }
    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(playerController.interactionKey))
            {
                playerController.money += playerController.wood;
                playerController.wood = 0;
                uiManager.UpdateMoney();
                uiManager.UpdateWood();
            }
        }
        
    }
}
