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

    public int hp = 20;
    public int maxHp = 20;

    public Slider hpBar;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        //���콺 Ŀ���� ESC ������������ ��ŵд�.
        Cursor.lockState = CursorLockMode.Locked;
        //���콺 Ŀ���� ������ ������ ������ �ʰ� ��
        //Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        //������� �Է��� �޴´�. 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //�̵� ���� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;
        //����ī�޶� �������� ������ ��ȯ
        dir = Camera.main.transform.TransformDirection(dir);

        if (cc.collisionFlags == CollisionFlags.Below)      //�ٴڿ� ������ �ִٸ� 
        {
            if (isJumpint) isJumpint = false;               //���� ���� ���̾��ٸ�  ���������� �ʱ�ȭ
            yVelocity = 0;
        }


        if(Input.GetKeyDown(KeyCode.Space) && !isJumpint) 
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
    }

    public void DamageAction(int damage)        //�÷��̾��� �ǰ� �Լ�
    { 
        //���ʹ��� ���ݷ� ��ŭ �÷��̾��� ü���� ��´�.
        hp -= damage;
       
    }
}
