using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition;     //�߻���ġ
    public GameObject bombFactory;    //����ź ������Ʈ
    public float throwPower = 15f;      //��ô �Ŀ�

    public GameObject bulletEffect;     //�ǰ� ����Ʈ ����Ʈ
    private ParticleSystem ps;          //�ǰ� ����Ʈ ��ƼŬ �ý���

    private LineRenderer lr;

    public int weaponPower = 5;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))    //������ ���콺 ��ư�� �������� 
        { 
            //����ź ������Ʈ ���� �� ����ź�� ������ġ�� �߻���ġ�� ����
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;

            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            //ī�޶��� ���� �������� ����ź�� �������� ���� ���Ѵ�. 

            //ForceMode
            //1. Porce  : �������� �� ������ ���� 0
            //2. Impulse : �������� �� , ������ ���� 0
            //3, VelocityChange : �������� �� , ������ ���� X
            //4. Accleration : �������� �� , ������ ���� X

            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }

        if(Input.GetMouseButtonDown (0))
        {
            //���̸� ������ �� �߻�� ��ġ�� ���� ���� ����
            Ray ray = new Ray(Camera.main.transform.position , Camera.main.transform.forward);
            RaycastHit hitInfo  = new RaycastHit();     //���̰� �΋H�� ������Ʈ�� ������ �����´�.

            lr.SetPosition(0, ray.origin);

            lr.enabled = true;
            //���̸� �߻��� �� ���� �΋H�� ��ü�� ������ ����Ʈ ǥ��
            if (Physics.Raycast(ray, out hitInfo))
            {
                //�΋H�� ��ü�� �ݶ��̴��� ������
                if (hitInfo.collider != null)
                {
                    //if (hitInfo.collider.TryGetComponent<EnemyFSM>(out EnemyFSM enemy))
                    //    enemy.HitEnemy(weaponPower);

                    //���̿� �΋H�� ������Ʈ�� Enemy���
                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        //���ʹ� ��ũ��Ʈ�� HitEnemy �Լ��� �����Ͽ� �÷��̾��� ������ ����
                        EnemyFSM eFSM = hitInfo.collider.gameObject.GetComponent<EnemyFSM>();
                        eFSM.HitEnemy(weaponPower);
                    }
                    //�΋H�� �������� �ǰ� ����Ʈ ��ġ ���� ����
                    bulletEffect.transform.position = hitInfo.point;
                    bulletEffect.transform.forward = hitInfo.normal;
                    ps.Play();

                    lr.SetPosition(1, hitInfo.point);
                }
            }
            else
            {
                lr.SetPosition(1, ray.direction * 200);
            }

            Invoke(nameof(lrEnable), 0.1f);
        }

    }

    private void lrEnable()
    {
        lr.enabled = false;
    }




}
