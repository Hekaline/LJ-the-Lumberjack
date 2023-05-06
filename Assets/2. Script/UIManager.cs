using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    PlayerController playerController;
    public TextMeshProUGUI tmpTreeHP;
    public TextMeshProUGUI tmpWood, tmpMoney;

    private void Start() {
        playerController = GameObject.Find("Player")
        .GetComponent<PlayerController>();
    }
    public void MoveToLobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }

    void Update()
    {
        if (playerController.wood >= playerController.maxWood)
        {
            tmpTreeHP.text = "Backpack is full!";
        }
    }

    public void UpdateTreeHealth(int nowHealth, int maxHealth)
    {
        tmpTreeHP.text = "Tree Health: " + nowHealth + "/" + maxHealth;
    }

    public void EraseTreeHealth()
    {
        tmpTreeHP.text = "";
    }

    public void UpdateWood()
    {
        tmpWood.text = "Wood: " + playerController.wood + "/" + playerController.maxWood;
    }

    public void UpdateMoney()
    {
        tmpMoney.text = "Money: " + playerController.money;
    }
}
