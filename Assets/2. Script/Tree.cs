using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    Hatchet hatchet;
    PlayerController playerController;
    AudioSource sound;
    public TextMeshProUGUI tmpHealth, tmpWood;

    int minDMG;
    int maxDMG;
    public int health;
    public int maxHealth;
    bool chopping = false;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = UnityEngine.Random.Range(80, 151);
        health = maxHealth;

        hatchet = GameObject.Find("Hatchet").GetComponent<Hatchet>();
        playerController = GameObject.Find("Body").GetComponent<PlayerController>();
        sound = gameObject.GetComponent<AudioSource>();

        minDMG = hatchet.minDMG;
        maxDMG = hatchet.maxDMG;

    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("swing"))
            chopping = false;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HATCHET" && 
            playerController.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("swing")
            && chopping == false)
        {
            health -= UnityEngine.Random.Range(minDMG, maxDMG);
            tmpHealth.text = "Tree Health: " + health + '/' + maxHealth;

            chopping = true;

            sound.Play();
        }

        if (health <= 0)
        {
            playerController.wood += UnityEngine.Random.Range(2, 5);
            print(playerController.wood);

            tmpHealth.text = "";
            tmpWood.text = "Woodd: " + playerController.wood;

            Destroy(gameObject);
        }
    }

}
