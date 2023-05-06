using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sell_Interaction : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    UIManager uiManager;
    [SerializeField]
    Button button;
    private void Start() {
        //playerController = Object.FindObjectOfType<PlayerController>();
        //uiManager = Object.FindObjectOfType<UIManager>();
    }
    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player")
        {
            button.gameObject.SetActive(true);
            if (Input.GetKeyDown(playerController.interactionKey))
            {
                SellTree();
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        button.gameObject.SetActive(false);
    }

    public void SellTree()
    {
        playerController.money += playerController.wood;
        playerController.wood = 0;
        uiManager.UpdateMoney();
        uiManager.UpdateWood();
    }

}
