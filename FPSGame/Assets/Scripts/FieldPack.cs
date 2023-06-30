using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPack : MonoBehaviour  // ���� �������� �����ϴ� ��ũ��Ʈ�Դϴ�.
{
    PlayerFire packCount;               // PlayerFire ��ũ��Ʈ�� �ִ� ���� ������ ���������� �����մϴ�.

    void Start()
    {
        packCount=FindObjectOfType<PlayerFire>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Player")
        {
            packCount.fieldItem_count++;    // PlayerFire ��ũ��Ʈ�� ���� ������ ���������� 1 �������� �ݴϴ�.

            Destroy(gameObject);            // ���� ������Ʈ�� �ı��մϴ�.
        }
    }
}
