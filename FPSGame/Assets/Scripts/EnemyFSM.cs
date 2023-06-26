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

    EnemyState m_State;               //���ʹ� ���� ����
    public float findDistance = 8f;     //�÷��̾� �߰� ����
    public float attackDistance = 2f;  //���ݰ��� ����
    public float moveSpeed = 5f;       //�̵� �ӵ�
    PlayerMove player;                   //�÷��̾� Ʈ������
    CharacterController cc;             //�ɸ��� ��Ʈ�ѷ� ������Ʈ�� �̿��� �̵�
    private float currentTime = 0f;   //�ð� ����
    private float attackDelay = 2f;    //���� ������ �ð�
    public float moveDistance = 20f;    //�̵����ɹ���
    private Vector3 originPos;          //�ʱ���ġ ����� ����
    public int attackPower = 3;       //Enemy�� ���ݷ�

    public int hp = 15;                         //���� ü��
    public int maxhp = 15;                //�ִ� ü��
    float distance = 0f;

    public Slider hpBar;
    
    void Start()
    {
        m_State = EnemyState.Idle;                                      //���ʹ��� ���� ��� ���´� ���
        player = FindObjectOfType<PlayerMove>();
        cc = GetComponent<CharacterController>();                   //�ɸ��� ��Ʈ�ѷ� ������Ʈ �Ҵ��ϱ�
        originPos = transform.position;                                     //�ڽ��� �ʱ���ġ ����
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
            Debug.Log("������ȯ Idle -> Move");
        }
    }
    private void Move() 
    {
        if (distance > moveDistance)                //20���� �۱��� �־������� ���� ���·� �����Ѵ�.
        {
            m_State = EnemyState.Return;
            print("���� ��ȯ : Move -> Return");
        }
        //���� �÷��̾���� �Ÿ��� ���� ���� ���̶�� �÷��̾ ���� �̵��Ѵ�.
        else if (distance > attackDistance)
        {
            //�÷��̾�� Enemy ��  ���� ����
            Vector3 dir = (player.transform.position - transform.position).normalized;
            //�̵�
            cc.Move(dir * Time.deltaTime * moveSpeed);
        }
        else
        {
            //���ݹ��� �����̶�� ���ݻ��·� ��ȯ
            m_State = EnemyState.Attack;
            print("������ȯ Move -> Attack");
            currentTime = attackDelay;      // �ٰ����� �ٷ� ���ݸ��� �����Բ� �����ð���  ���ݵ����� �ð���ŭ �̸� ���� ���ѳ��´�.
        }
    }
    private void Attack() 
    {
        if (distance < attackDistance)
        {
            //������ �ð� ���� �÷��̾ �����Ѵ�.
            currentTime += Time.deltaTime;
            if (currentTime >= attackDelay)
            {
                player.DamageAction(attackPower);
                print("����");
                currentTime = 0;
            }
        }
        else
        {
            //���ݹ��� ���̶�� �ٽ� ���� ���·� ��ȯ.
            m_State = EnemyState.Move;
            print("������ȯ Attack -> Move");
            currentTime = 0;
        }
    }
    private void Return()
    {
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            //���ư� ��ġ �� ������ ���� �� �̵�
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * Time.deltaTime * moveSpeed);
        }
        else
        {
            //0.1 ���� ��� �ʱ���ġ�� ���� �����ߴٸ� 
            transform.position = originPos;             //��Ȯ�� �ʱ���ġ�� ����
            hp = maxhp;                                 //hp�� �ٽ� ȸ��
            m_State = EnemyState.Idle;
            print("������ȯ Return  -> IDle");
        }
    }
    public void HitEnemy(int hitPoint)
    {
        //���� �̹� �ǰ� �����̰ų� ������� ��� �ƹ�ó���� �����ʰ� �Լ� ����
        if(m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        { return; }

        hp -= hitPoint;     //�÷��̾��� ���ݷ� ��ŭ ���ʹ��� ü���� ��´�.
        if (hp > 0)         //Enemy�� hp �� 0 �̻��̶�� 
        {
            //����ִٸ� ���̹̻��·� ��ȯ
            m_State = EnemyState.Damaged;
            print("���� ��ȯ Ani State -> Damaged");
            Damaged();
        }
        else
        {
            //�׾��ٸ� DIe ���·� ��ȯ
            m_State = EnemyState.Die;
            print("������ȯ Ani State -> Die");
            Die();
        }
    }

    private void Damaged() 
    {
        StartCoroutine(nameof(DmageProcess));           //�ǰ� ���¸� ó���ϱ� ���� �ڷ�ƾ�� ����
    }

    IEnumerator DmageProcess()
    {
        //�ǰݸ�Ǹ�ŭ ��ٸ���.
        yield return new WaitForSeconds(0.5f);
        m_State = EnemyState.Move;      //���� ���¸� �̵����·� ��ȯ
        print("������ȯ Damaged -> Move");
    }
    private void Die() 
    {
        //�״� ��� �� �������
        m_State = EnemyState.Die;
        StopAllCoroutines();    //�������� �ǰ� �ڷ�ƾ�� �����Ѵ�.
        StartCoroutine(nameof(DieProcess));
        print("������ȯ AniState -> Die");
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;
        yield return new WaitForSeconds(2f);
        print("�Ҹ�");
        Destroy(gameObject);
    }

}
