using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
        public GameObject bombEffect;       //���� ����Ʈ ������
        private void OnCollisionEnter(Collision collision)
        {
                GameObject eff = Instantiate(bombEffect);   //����Ʈ ������ ����
                eff.transform.position = transform.position;     //����ź ��ġ�� ����Ʈ ��ġ�� �����ϰ� ����
                Destroy(gameObject);                            //�浹 ������ ���� ����
        }


}
