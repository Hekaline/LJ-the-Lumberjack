using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{   
    public int nowHealth;
    public int maxHealth;
    public int woodAmt;
    
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = UnityEngine.Random.Range(80, 151);
        nowHealth = maxHealth;
        woodAmt = UnityEngine.Random.Range(2,5);
    }

    
}
