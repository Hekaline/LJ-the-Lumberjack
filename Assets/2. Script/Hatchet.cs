using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hatchet : MonoBehaviour
{
    public int minDMG;
    public int maxDMG;
    [SerializeField]
    HandController handController;
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    public UIManager uiManager;
    Tree tree;
    [SerializeField]
    AudioSource sound;
    public bool isChopping = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "TREE" &&
            handController.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("swing")
            && isChopping == false)
        {
            tree = other.gameObject.GetComponent<Tree>();
            isChopping = true;
            sound = tree.GetComponent<AudioSource>();
            sound.Play();
            StopCoroutine("WaitUntilStopChopping");
            StartCoroutine("WaitUntilStopChopping");

           
            tree.nowHealth -= UnityEngine.Random.Range(minDMG, maxDMG);
            uiManager.UpdateTreeHealth(tree.nowHealth, tree.maxHealth);
            
            if (tree.nowHealth <= 0)
            {
                playerController.wood += tree.woodAmt;
                if (playerController.wood >= playerController.maxWood)
                    playerController.wood = playerController.maxWood;

                uiManager.EraseTreeHealth();
                uiManager.UpdateWood();

                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator WaitUntilStopChopping()
    {
        yield return new WaitForSeconds
            (handController.GetComponent<Animator>().
            GetCurrentAnimatorClipInfo(0).Length - 0.1f); 
        isChopping = false;
    }
}
