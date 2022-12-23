using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tree : MonoBehaviour
{
    Hatchet hatchet;
    int minDMG;
    int maxDMG;
    public int health;
    bool chopping = false;

    // Start is called before the first frame update
    void Start()
    {
        health = UnityEngine.Random.Range(80, 151);
        hatchet = GameObject.Find("Hatchet").GetComponent<Hatchet>();

        minDMG = hatchet.minDMG;
        maxDMG = hatchet.maxDMG;

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("swing")
            && chopping == false)
        {
            health -= UnityEngine.Random.Range(minDMG, maxDMG);
            chopping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        chopping = false;
    }
}
