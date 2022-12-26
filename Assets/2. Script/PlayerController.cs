using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

        if (Input.GetKeyDown(KeyCode.LeftShift) // 달리기 키를 눌렀고
            && nowStamina > 0                   // 스태미나가 있고
            && (hAxis != 0 || vAxis != 0))      // 이동하고 있으면
        {
            isRunning = true;
            nowSpeed *= runMultiply; // 달린다(속도 변경, isRunning = true)
        }

        if (Input.GetKey(KeyCode.LeftShift) 
            && nowStamina > 0 
            && (hAxis != 0 || vAxis != 0)
            && isRunning == true)
        {
            nowStamina -= 10 * Time.deltaTime; // 달리고 있으면 스태미나 감소
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) ||
            !Input.GetKey(KeyCode.LeftShift) ||
            nowStamina <= 0f || (hAxis == 0 && vAxis == 0) ||
            isRunning == false) // shift키를 누르지 않거나 떼거나 스태미나가 없거나 움직이지 않거나 isRunning이 false면
        {
            isRunning = false;
            nowSpeed = walkSpeed;
            if (nowStamina < 100f)
                nowStamina += 5 * Time.deltaTime;
        } // 걷는다

        staminaGauge.fillAmount = nowStamina / maxStamina; // 이미지 채우기(스태미나 게이지바)

        
        if (hAxis != 0 || vAxis != 0) // 움직이고 있으면
        {
            bodyAnimator.SetBool("onMoving", true);
            playerRotation.x = nowSpeed * 2;
            transform.rotation = Quaternion.Euler(playerRotation); // 고개를 숙인다
            
        }
        else // 아니면 고개 들기
        {
            bodyAnimator.SetBool("onMoving", false);
            playerRotation.x = 0;
            transform.rotation = Quaternion.Euler(playerRotation);
        }


        if (handController.GetComponent<Animator>().
            GetCurrentAnimatorStateInfo(0).IsName("swing")) // 플레이어가 swing 모션을 취하고 있으면 
        {
            bodyAnimator.SetBool("onChopping", true); //onChopping = true
            StopCoroutine("Wait"); // 모션 길이만큼 기다리는 코루틴 실행(기다린 후 onChopping이 false로 변함)
            StartCoroutine("Wait");

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(
            handController.GetComponent<Animator>().
            GetCurrentAnimatorClipInfo(0).Length);

        bodyAnimator.SetBool("onChopping", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "EnterHouseTrigger")
            SceneManager.LoadScene("House");
    }
}
