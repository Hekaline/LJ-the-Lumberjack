using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Animator bodyAnimator;
    HandController handController;
    PlayerController playerController;
    Rigidbody charRigidbody;
    
    public float moveSpeed = 5f;
    public int wood = 0;
    // Start is called before the first frame update
    void Start()
    {
        bodyAnimator = GameObject.Find("Body").GetComponent<Animator>();
        handController = GameObject.Find("Hand").GetComponent<HandController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        charRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 
            || handController.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("swing"))
        {
            bodyAnimator.SetBool("onMoving", true);
            StopCoroutine("Wait");
            StartCoroutine("Wait");
            
        }


        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 dd
            = new Vector3(hAxis, 0, vAxis);

        charRigidbody.velocity= dd * moveSpeed;
        
        transform.LookAt(transform.position + dd);
    }

    IEnumerator Wait()
    {
        print("waiting...");
        yield return new WaitForSeconds(0.5f);
        bodyAnimator.SetBool("onMoving", false);
    }
}
