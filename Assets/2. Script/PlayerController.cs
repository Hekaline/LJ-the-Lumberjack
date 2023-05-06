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
    public int money = 0;
    public int maxWood = 20; // ���� ����
    
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
            = new Vector3(hAxis, 0, vAxis).normalized; // normalized: ���� �밢�� �ӵ��� ������ �ӵ��� ���� ����

        charRigidbody.velocity = moveDir * nowSpeed; // ĳ���� �ӵ�

        transform.LookAt(transform.position + moveDir); //ĳ���Ͱ� ���� ���� �ٶ󺸱�

        playerRotation = transform.eulerAngles;

        if (Input.GetKeyDown(KeyCode.LeftShift) // �޸��� Ű�� ������
            && nowStamina > 0                   // ���¹̳��� �ְ�
            && (hAxis != 0 || vAxis != 0))      // �̵��ϰ� ������
        {
            isRunning = true;
            nowSpeed *= runMultiply; // �޸���(�ӵ� ����, isRunning = true)
        }

        if (Input.GetKey(KeyCode.LeftShift) 
            && nowStamina > 0 
            && (hAxis != 0 || vAxis != 0)
            && isRunning == true)
        {
            nowStamina -= 10 * Time.deltaTime; // �޸��� ������ ���¹̳� ����
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) ||
            !Input.GetKey(KeyCode.LeftShift) ||
            nowStamina <= 0f || (hAxis == 0 && vAxis == 0) ||
            isRunning == false) // shiftŰ�� ������ �ʰų� ���ų� ���¹̳��� ���ų� �������� �ʰų� isRunning�� false��
        {
            isRunning = false;
            nowSpeed = walkSpeed;
            if (nowStamina < 100f)
                nowStamina += 5 * Time.deltaTime;
        } // �ȴ´�

        staminaGauge.fillAmount = nowStamina / maxStamina; // �̹��� ä���(���¹̳� ��������)

        
        if (hAxis != 0 || vAxis != 0) // �����̰� ������
        {
            bodyAnimator.SetBool("onMoving", true);
            playerRotation.x = nowSpeed * 2;
            transform.rotation = Quaternion.Euler(playerRotation); // ������ ���δ�
            
        }
        else // �ƴϸ� ���� ���
        {
            bodyAnimator.SetBool("onMoving", false);
            playerRotation.x = 0;
            transform.rotation = Quaternion.Euler(playerRotation);
        }


        if (handController.GetComponent<Animator>().
            GetCurrentAnimatorStateInfo(0).IsName("swing")) // �÷��̾ swing ����� ���ϰ� ������ 
        {
            bodyAnimator.SetBool("onChopping", true); //onChopping = true
            StopCoroutine("Wait"); // ��� ���̸�ŭ ��ٸ��� �ڷ�ƾ ����(��ٸ� �� onChopping�� false�� ����)
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
