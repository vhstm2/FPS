using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;
    private CharacterController cc;         //�̵� , ���� , �߷±���            //(������ٵ� ���� �ξ� ������)

    private float gravity = -20f;              //�߷º���
    private float yVelocity = 0;                 //�����ӷº���
    private float jumpPower = 10f;        //������ �Ŀ�
    private bool isJumpint = false;         //���� ���� 

    public int hp = 20;                             //���� HP
    public int maxHp = 20;                  //�ִ� HP

    public Slider hpBar;                        //hp �����̴�

    public GameObject hitEffect;    //hit ȿ�� ������Ʈ

    [NonSerialized] public Animator anim;

    #region
    public float durationTime = 5f;             // �̵��ӵ� ���� �⺻���ӽð��Դϴ�.
    public float holdingTime = 0f;              // �̵��ӵ� ���� �����ð��Դϴ�.
    public int dumyHp = 20;                     // ���� ������ ü������� ����ü�� �������Դϴ�.

    PlayerFire Invi;                            // ���� ���� ���θ� Ȯ���ϱ� ���� �������Դϴ�.
    #endregion

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        
    }

    private void Start()
    {
        #region
        Invi = GetComponent<PlayerFire>();           // Invi �ʱ�ȭ�Դϴ�.
        #endregion
    }
    void Update()
    {
        //���� �� ���°� �ƴϸ� ���� ���� ����
        if (GameManager.Instance.gState != GameManager.GameState.Run) return;


        //������� �Է��� �޴´�. 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //�̵� ���� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;
        //����ī�޶� �������� ������ ��ȯ
        dir = Camera.main.transform.TransformDirection(dir);

        anim.SetFloat("IdleToRun", dir.magnitude);
        


        if (cc.collisionFlags == CollisionFlags.Below)      //�ٴڿ� ������ �ִٸ� 
        {
            if (isJumpint) isJumpint = false;               //���� ���� ���̾��ٸ�  ���������� �ʱ�ȭ
            yVelocity = 0;
        }


        if (Input.GetKeyDown(KeyCode.Space) && !isJumpint)
        {
            yVelocity = jumpPower;
            isJumpint = true;
        }

        //ĳ���� ���� �ӵ��� �߷� ���� �����Ѵ�.
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);      //�̵��ӵ��� ���� �̵��Ѵ�.
                                                        //transform.position += dir * moveSpeed * Time.deltaTime; 

        //���� �÷��̾��� ü���� ������� ����Ͽ� value �� �����Ѵ�.
        hpBar.value = (float)hp / (float)maxHp;

        #region
        holdingTime += Time.deltaTime;  // �̵��ӵ� ���� ������ ���ӽð� üũ���Դϴ�.


        if (holdingTime > durationTime) // �����ð��� �⺻���ӽð����� ��ٸ�
        {
            moveSpeed = 7f;             // �̵��ӵ��� 7f������ �ʱ�ȭ �մϴ�.
        }
        #endregion

    }

    public void DamageAction(int damage)        //�÷��̾��� �ǰ� �Լ�
    {
        //���ʹ��� ���ݷ� ��ŭ �÷��̾��� ü���� ��´�.
        hp -= damage;
        #region
        if (Invi.fieldItemUse == false) // ������������ ������� ���� ���¶��
            dumyHp = hp;                // ���� ü�¿� ���� ü�°��� �����մϴ�.
        #endregion
        if (hp > 0) StartCoroutine(nameof(hitProcess));
        else
        { 
            //�÷��̾ �׾�����  �ĺ� 1 

        }
    }

    IEnumerator hitProcess()
    {
            yield return null;
            hitEffect.gameObject.SetActive(true);       //�ǰ� UI Ȱ��ȭ
            yield return new WaitForSeconds(0.3f);      //0.3�� ���
            hitEffect.gameObject.SetActive(false);      //�ǰ� UI ��Ȱ��ȭ
    }


}
