using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System;

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

  [SerializeField]  EnemyState m_State;               //���ʹ� ���� ����
    public float findDistance = 8f;     //�÷��̾� �߰� ����
    public float attackDistance = 2f;  //���ݰ��� ����
    public float moveSpeed = 5f;       //�̵� �ӵ�
    [SerializeField] PlayerMove player;                   //�÷��̾� Ʈ������
    CharacterController cc;             //�ɸ��� ��Ʈ�ѷ� ������Ʈ�� �̿��� �̵�
    private float currentTime = 0f;   //�ð� ����
    private float attackDelay = 2f;    //���� ������ �ð�
    public float moveDistance = 50f;    //�̵����ɹ���
    private Vector3 originPos;          //�ʱ���ġ ����� ����
    public int attackPower = 3;       //Enemy�� ���ݷ�

    public int hp = 30;                         //���� ü��
    public int maxhp = 30;                //�ִ� ü��
    float distance = 0f;

    public Slider hpBar;                            //hp bar  �����̴� 

    [NonSerialized] public Animator anim;                  //�ִϸ����� ������Ʈ ���� (�ִϸ��̼��� �����Ű������ ����
    Quaternion originRot;
 
    NavMeshAgent smith;

    public GameObject headShotsprite;
    public GameObject headObject;

    public GameObject bloodEffect;

    public void HeadShoot()
    {
        StartCoroutine(headShotProcess(0.3f));
    }

    private IEnumerator headShotProcess(float t)
    {
        headShotsprite.transform.position = headObject.transform.position + Vector3.up * 1.5f;
        headShotsprite.transform.LookAt(player.transform.position);
        headShotsprite.SetActive(true);
        yield return new WaitForSeconds(t);
        headShotsprite.SetActive(false);
    }


    void Start()
    {
        m_State = EnemyState.Idle;                                      //���ʹ��� ���� ��� ���´� ���
        cc = GetComponent<CharacterController>();                   //�ɸ��� ��Ʈ�ѷ� ������Ʈ �Ҵ��ϱ�
        originPos = transform.position;                                     //�ڽ��� �ʱ���ġ ����
        originRot = transform.rotation;
        player = FindObjectOfType<PlayerMove>();
        anim = transform.GetComponentInChildren<Animator>();
        smith = GetComponent<NavMeshAgent>();       //�׺���̼� ������Ʈ ������Ʈ �޾ƿ���.

        //�ڵ� �̵� ����
      //  smith.updatePosition = false;
        //�ڵ� ȸ������
      //  smith.updateRotation = false;
    }

    void Update()
    {
        //float distance = (player.position - transform.position).magnitude;
        if (player != null)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
        }
       
        switch (m_State)
        {
            case EnemyState.Idle: Idle(); break;
            case EnemyState.Move: Move(); break;
            case EnemyState.Attack: Attack(); break;
            case EnemyState.Return: Return(); break;
                //  case EnemyState.Damaged:  Damaged();  break;
                //  case EnemyState.Die:  Die();  break;
        }

        hpBar.value = (float)hp / (float)maxhp;
    }

    private void Idle()
    {
        if (distance < findDistance)
        {
            anim.SetTrigger("IdleToMove");
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
            //Vector3 dir = (player.transform.position - transform.position).normalized;
            //�̵�
           // cc.Move(dir * Time.deltaTime * moveSpeed);
           // transform.forward = dir;

            //�׺���̼� ������Ʈ�� �̵��� ���߰� ��θ� �ʱ�ȭ 
            smith.isStopped = true;
            smith.ResetPath();
            smith.stoppingDistance = attackDistance;        //���� ������ �Ÿ��� �ּҰŸ�
            smith.destination = player.transform.position;      //���̰��̼��� ������

           

        }
        else
        {
            //���ݹ��� �����̶�� ���ݻ��·� ��ȯ
            m_State = EnemyState.Attack;
            print("������ȯ Move -> Attack");
            currentTime = attackDelay;      // �ٰ����� �ٷ� ���ݸ��� �����Բ� �����ð���  ���ݵ����� �ð���ŭ �̸� ���� ���ѳ��´�.
            anim.SetTrigger("MoveToAttackDelay");
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
           //     player.DamageAction(attackPower);
                print("����");
                currentTime = 0;
                //AttackDelay���� Attack���� �Ѿ ���� 2�ʿ� �ѹ��� ������ ���� �ĸ����Ͱ� �ʿ���.
                //Attack���� AttackDelay�� �Ķ���Ͱ� �ʿ���� (�ڵ���ȯ)
                anim.SetTrigger("StartAttack");
            }
        }
        else
        {
            //���ݹ��� ���̶�� �ٽ� ���� ���·� ��ȯ.
            m_State = EnemyState.Move;
            print("������ȯ Attack -> Move");
            currentTime = 0;
            anim.SetTrigger("AttackToMove");
        }
    }
    private void Return()
    {
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            //���ư� ��ġ �� ������ ���� �� �̵�
            //Vector3 dir = (originPos - transform.position).normalized;
            //cc.Move(dir * Time.deltaTime * moveSpeed);
            //transform.forward = dir;

            //�׺���̼� ������Ʈ�� �̵��� ���߰� ��θ� �ʱ�ȭ 
            smith.stoppingDistance = 0;        //���� ������ �Ÿ��� �ּҰŸ�
            smith.destination = originPos;      //���̰��̼��� ������


        }
        else
        {
            
            smith.isStopped = true;         //�̵�����
            smith.ResetPath();                  //��� ����

            //0.1 ���� ��� �ʱ���ġ�� ���� �����ߴٸ� 
            transform.position = originPos;             //��Ȯ�� �ʱ���ġ�� ����
            hp = maxhp;                                 //hp�� �ٽ� ȸ��
            m_State = EnemyState.Idle;
            print("������ȯ Return  -> IDle");
            anim.SetTrigger("MoveToIdle");      //�ʱ���ġ��  ���ƿ����� ��� �ִϸ��̼� ����
            transform.rotation = originRot;
        }
    }
    public void HitEnemy(int hitPoint)
    {
        //���� �̹� �ǰ� �����̰ų� ������� ��� �ƹ�ó���� �����ʰ� �Լ� ����
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        { return; }

        hp -= hitPoint;     //�÷��̾��� ���ݷ� ��ŭ ���ʹ��� ü���� ��´�.
        smith.ResetPath();
        smith.isStopped = true;
        if (hp > 0)         //Enemy�� hp �� 0 �̻��̶�� 
        {
            //����ִٸ� ���̹̻��·� ��ȯ
            m_State = EnemyState.Damaged;
            print("���� ��ȯ Ani State -> Damaged");
            anim.SetTrigger("Damaged");
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
        yield return new WaitForSeconds(0.1f);
        m_State = EnemyState.Move;      //���� ���¸� �̵����·� ��ȯ
        print("������ȯ Damaged -> Move");
    }





    private void Die()
    {
        smith.ResetPath();
        smith.isStopped = true;
        //�״� ��� �� �������
        m_State = EnemyState.Die;
        StopAllCoroutines();    //�������� �ǰ� �ڷ�ƾ�� �����Ѵ�.
        StartCoroutine(nameof(DieProcess));
        print("������ȯ AniState -> Die");
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(2f);
        print("�Ҹ�");
        Destroy(gameObject);
    }


    /// <summary>
    /// HitEvent���� ȣ���� �Լ� , HitEvent �� Zombie �� Z_Attack �ִϸ��̼��� 10��° �����ӿ��� ���
    /// </summary>
    public void AttackAction()
    {
            player.DamageAction(attackPower);
    }

    public void BloodEffect(Vector3 hitPoint, Vector3 hitNormal)
    {
        //�ǰ� �� Ray�� ����  ��ǥ �� ȸ�� ���� �޾ƿͼ� �ǰ� ������Ʈ�� �����ѵ� 0.3�� �� �ı�
        GameObject blood = Instantiate(bloodEffect, hitPoint , Quaternion.LookRotation(hitNormal) );
        blood.transform.SetParent(transform);
        
        Destroy(blood ,0.3f);
    }

}
