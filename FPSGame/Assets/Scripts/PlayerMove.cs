using System;
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

    public int hp = 20;                             //현재 HP
    public int maxHp = 20;                  //최대 HP

    public Slider hpBar;                        //hp 슬라이더

    public GameObject hitEffect;    //hit 효과 오브젝트

    [NonSerialized] public Animator anim;

    #region
    public float durationTime = 5f;             // 이동속도 증가 기본지속시간입니다.
    public float holdingTime = 0f;              // 이동속도 증가 유지시간입니다.
    public int dumyHp = 20;                     // 무적 구현을 체력저장용 더미체력 데이터입니다.

    PlayerFire Invi;                            // 무적 상태 여부를 확인하기 위한 데이터입니다.
    #endregion

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        
    }

    private void Start()
    {
        #region
        Invi = GetComponent<PlayerFire>();           // Invi 초기화입니다.
        #endregion
    }
    void Update()
    {
        //게임 중 생태가 아니면 전부 실행 금지
        if (GameManager.Instance.gState != GameManager.GameState.Run) return;


        //사용자의 입력을 받는다. 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //이동 방향 설정
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;
        //메인카메라를 기준으로 방향을 변환
        dir = Camera.main.transform.TransformDirection(dir);

        anim.SetFloat("IdleToRun", dir.magnitude);
        


        if (cc.collisionFlags == CollisionFlags.Below)      //바닥에 착지해 있다면 
        {
            if (isJumpint) isJumpint = false;               //만일 점프 중이었다면  점프전으로 초기화
            yVelocity = 0;
        }


        if (Input.GetKeyDown(KeyCode.Space) && !isJumpint)
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

        #region
        holdingTime += Time.deltaTime;  // 이동속도 증가 아이템 지속시간 체크용입니다.


        if (holdingTime > durationTime) // 유지시간이 기본지속시간보다 길다면
        {
            moveSpeed = 7f;             // 이동속도를 7f값으로 초기화 합니다.
        }
        #endregion

    }

    public void DamageAction(int damage)        //플레이어의 피격 함수
    {
        //에너미의 공격력 만큼 플레이어의 체력을 깍는다.
        hp -= damage;
        #region
        if (Invi.fieldItemUse == false) // 무적아이템을 사용하지 않은 상태라면
            dumyHp = hp;                // 더미 체력에 현재 체력값을 저장합니다.
        #endregion
        if (hp > 0) StartCoroutine(nameof(hitProcess));
        else
        { 
            //플레이어가 죽었을때  후보 1 

        }
    }

    IEnumerator hitProcess()
    {
            yield return null;
            hitEffect.gameObject.SetActive(true);       //피격 UI 활성화
            yield return new WaitForSeconds(0.3f);      //0.3초 대기
            hitEffect.gameObject.SetActive(false);      //피격 UI 비활성화
    }


}
