using UnityEngine;


public class PlayerController : MonoBehaviour
{
    Animator animator;
    Rigidbody charRigidbody;
    public float moveSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        charRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("onClick");
        }

        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        if (Input.GetAxis("Horizontal") != 0|| Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        Vector3 inputDir
            = new Vector3(hAxis, 0, vAxis).normalized;

        charRigidbody.velocity = inputDir * moveSpeed * Time.deltaTime;
        
        transform.LookAt(transform.position + inputDir);
    }
}
