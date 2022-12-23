using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody charRigidbody;
    public float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        charRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("onClick");
        }

        

        //if (Input.GetAxis("Horizontal") != 0|| Input.GetAxis("Vertical") != 0)
        //{
        //    animator.SetBool("isMoving", true);
        //}
        //else
        //{
        //    animator.SetBool("isMoving", false);
        //}

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 dd
            = new Vector3(hAxis, 0, vAxis);

        charRigidbody.velocity= dd * moveSpeed;
        
        transform.LookAt(transform.position + dd);
    }
}
