using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandController : MonoBehaviour
{
    Animator animator;
    Hatchet hatchet;
    private void Start()
    {
        animator = GetComponent<Animator>();
        hatchet = FindObjectOfType<Hatchet>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && hatchet.isChopping == false)
            animator.SetTrigger("onClick");
    }
}
