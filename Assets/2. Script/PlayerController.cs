using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    Animator bodyAnimator;
    HandController handController;
    Rigidbody charRigidbody;
    public Image staminaGauge;
    
    public float nowSpeed = 5f;
    public float walkSpeed = 5f;
    public bool isRunning = false;
    public float runMultiply = 1.5f;
    public float maxStamina = 100f;
    public float nowStamina = 100f;
    public int wood = 0;
    
    Vector3 playerRotation;
    // Start is called before the first frame update
    void Start()
    {
        bodyAnimator = GameObject.Find("Body").GetComponent<Animator>();
        handController = GameObject.Find("Hand").GetComponent<HandController>();
        charRigidbody = GetComponent<Rigidbody>();

        staminaGauge.type = Image.Type.Filled;
        staminaGauge.fillMethod = Image.FillMethod.Horizontal;
        staminaGauge.fillOrigin = (int)Image.OriginHorizontal.Left;
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");        

        Vector3 moveDir
            = new Vector3(hAxis, 0, vAxis).normalized; // normalized: 대충 대각선 속도를 일직선 속도와 같게 해줌

        charRigidbody.velocity = moveDir * nowSpeed; // 캐릭터 속도

        transform.LookAt(transform.position + moveDir); //캐릭터가 가는 방향 바라보기

        playerRotation = transform.eulerAngles;

        if (Input.GetKeyDown(KeyCode.LeftShift) && nowStamina > 0 && (hAxis == 0 && vAxis == 0))
        {
            isRunning = true;
            nowSpeed *= runMultiply;
        }
        if (Input.GetKey(KeyCode.LeftShift) && nowStamina > 0)
        {
            nowStamina -= 10 * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || !Input.GetKey(KeyCode.LeftShift) || nowStamina <= 0 || (hAxis == 0 && vAxis == 0))
        {
            isRunning = false;
            nowSpeed = walkSpeed;
            if (nowStamina < 100f)
                nowStamina += 5 * Time.deltaTime;
        }
        staminaGauge.fillAmount = nowStamina / maxStamina;

        
        if (hAxis != 0 || vAxis != 0)
        {
            bodyAnimator.SetBool("onMoving", true);
            playerRotation.x = nowSpeed * 2;
            transform.rotation = Quaternion.Euler(playerRotation);
            
        }
        else
        {
            bodyAnimator.SetBool("onMoving", false);
            playerRotation.x = 0;
            transform.rotation = Quaternion.Euler(playerRotation);
        }
        if (handController.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("swing"))
        {
            bodyAnimator.SetBool("onChopping", true);
            StopCoroutine("Wait");
            StartCoroutine("Wait");

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(handController.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
        bodyAnimator.SetBool("onChopping", false);
    }
}
