using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{

    enum EnemyState
    { 
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    EnemyState m_State;               //에너미 상태 변수
    public float findDistance = 8f;     //플레이어 발견 범위
    public float attackDistance = 2f;  //공격가능 범위
    public float moveSpeed = 5f;       //이동 속도
    PlayerMove player;                   //플레이어 트랜스폼
    CharacterController cc;             //케릭터 컨트롤러 컴포넌트를 이용한 이동
    private float currentTime = 0f;   //시간 누적
    private float attackDelay = 2f;    //공격 딜레이 시간
    public float moveDistance = 20f;    //이동가능범위
    private Vector3 originPos;          //초기위치 저장용 변수
    public int attackPower = 3;       //Enemy의 공격력

    public int hp = 15;                         //현재 체력
    public int maxhp = 15;                //최대 체력
    float distance = 0f;

    public Slider hpBar;
    
    void Start()
    {
        m_State = EnemyState.Idle;                                      //에너미의 최초 대기 상태는 대기
        player = FindObjectOfType<PlayerMove>();
        cc = GetComponent<CharacterController>();                   //케릭터 컨트롤러 컴포넌트 할당하기
        originPos = transform.position;                                     //자신의 초기위치 저장
    }

    void Update()
    {
            //float distance = (player.position - transform.position).magnitude;
            distance = Vector3.Distance(player.transform.position , transform.position);
            switch (m_State)
            {
                case EnemyState.Idle:  Idle(); break;
                case EnemyState.Move:  Move(); break;
                case EnemyState.Attack:  Attack();  break;
                case EnemyState.Return:  Return(); break;
              //  case EnemyState.Damaged:  Damaged();  break;
              //  case EnemyState.Die:  Die();  break;
            }

            hpBar.value = (float)hp / (float)maxhp;
    }

    private void Idle()
    {
        if (distance < findDistance)
        {
            m_State = EnemyState.Move;
            Debug.Log("상태전환 Idle -> Move");
        }
    }
    private void Move() 
    {
        if (distance > moveDistance)                //20미터 밖까지 멀어졌으면 원래 상태로 복귀한다.
        {
            m_State = EnemyState.Return;
            print("상태 전환 : Move -> Return");
        }
        //만일 플레이어와의 거리가 공격 범위 밖이라면 플레이어를 행해 이동한다.
        else if (distance > attackDistance)
        {
            //플레이어와 Enemy 의  방향 설정
            Vector3 dir = (player.transform.position - transform.position).normalized;
            //이동
            cc.Move(dir * Time.deltaTime * moveSpeed);
        }
        else
        {
            //공격범위 안쪽이라면 공격상태로 전환
            m_State = EnemyState.Attack;
            print("상태전환 Move -> Attack");
            currentTime = attackDelay;      // 다가가면 바로 공격먼저 나오게끔 누적시간을  공격딜레이 시간만큼 미리 진행 시켜놓는다.
        }
    }
    private void Attack() 
    {
        if (distance < attackDistance)
        {
            //일정한 시간 마다 플레이어를 공격한다.
            currentTime += Time.deltaTime;
            if (currentTime >= attackDelay)
            {
                player.DamageAction(attackPower);
                print("공격");
                currentTime = 0;
            }
        }
        else
        {
            //공격범위 밖이라면 다시 무브 상태로 전환.
            m_State = EnemyState.Move;
            print("상태전환 Attack -> Move");
            currentTime = 0;
        }
    }
    private void Return()
    {
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            //돌아갈 위치 의 방향을 구한 뒤 이동
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * Time.deltaTime * moveSpeed);
        }
        else
        {
            //0.1 이하 라면 초기위치랑 거의 근접했다면 
            transform.position = originPos;             //정확한 초기위치로 지정
            hp = maxhp;                                 //hp를 다시 회복
            m_State = EnemyState.Idle;
            print("상태전환 Return  -> IDle");
        }
    }
    public void HitEnemy(int hitPoint)
    {
        //만일 이미 피격 상태이거나 사망상태 라면 아무처리를 하지않고 함수 종료
        if(m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        { return; }

        hp -= hitPoint;     //플레이어의 공격력 만큼 에너미의 체력을 깍는다.
        if (hp > 0)         //Enemy의 hp 가 0 이상이라면 
        {
            //살아있다면 데이미상태로 변환
            m_State = EnemyState.Damaged;
            print("상태 전환 Ani State -> Damaged");
            Damaged();
        }
        else
        {
            //죽었다면 DIe 상태로 변환
            m_State = EnemyState.Die;
            print("상태전환 Ani State -> Die");
            Die();
        }
    }

    private void Damaged() 
    {
        StartCoroutine(nameof(DmageProcess));           //피격 상태를 처리하기 위한 코루틴을 실행
    }

    IEnumerator DmageProcess()
    {
        //피격모션만큼 기다린다.
        yield return new WaitForSeconds(0.5f);
        m_State = EnemyState.Move;      //현재 상태를 이동상태로 전환
        print("상태전환 Damaged -> Move");
    }
    private void Die() 
    {
        //죽는 모션 후 사라지기
        m_State = EnemyState.Die;
        StopAllCoroutines();    //진행중인 피격 코루틴을 중지한다.
        StartCoroutine(nameof(DieProcess));
        print("상태전환 AniState -> Die");
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;
        yield return new WaitForSeconds(2f);
        print("소멸");
        Destroy(gameObject);
    }

}
