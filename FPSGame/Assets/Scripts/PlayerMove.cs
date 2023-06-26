using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;
    private CharacterController cc;         //이동 , 점프 , 중력구현            //(리지드바디에 비해 훨씬 가볍다)

    private float gravity = -20f;              //중력변수
    private float yVelocity = 0;                 //수직속력변수
    private float jumpPower = 10f;        //점프력 파워
    private bool isJumpint = false;         //점프 상태 

    public int hp = 20;
    public int maxHp = 20;

    public Slider hpBar;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        //마우스 커서를 ESC 누리기전까지 잠궈둔다.
        Cursor.lockState = CursorLockMode.Locked;
        //마우스 커서를 윈도우 밖으로 나가지 않게 함
        //Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        //사용자의 입력을 받는다. 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //이동 방향 설정
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;
        //메인카메라를 기준으로 방향을 변환
        dir = Camera.main.transform.TransformDirection(dir);

        if (cc.collisionFlags == CollisionFlags.Below)      //바닥에 착지해 있다면 
        {
            if (isJumpint) isJumpint = false;               //만일 점프 중이었다면  점프전으로 초기화
            yVelocity = 0;
        }


        if(Input.GetKeyDown(KeyCode.Space) && !isJumpint) 
        {
            yVelocity = jumpPower;
            isJumpint = true;
        }

        //캐릭터 수직 속도에 중력 값을 적용한다.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);      //이동속도에 맞춰 이동한다.
        //transform.position += dir * moveSpeed * Time.deltaTime; 

        //현재 플레이어의 체력을 백분율로 계산하여 value 에 저장한다.
        hpBar.value = (float)hp / (float)maxHp;
    }

    public void DamageAction(int damage)        //플레이어의 피격 함수
    { 
        //에너미의 공격력 만큼 플레이어의 체력을 깍는다.
        hp -= damage;
       
    }
}
