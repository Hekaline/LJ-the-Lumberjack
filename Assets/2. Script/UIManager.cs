using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI treeHP;

    public void MoveToLobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }
}
