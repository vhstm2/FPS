using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPack : MonoBehaviour  // �̵��ӵ� ���� �������� �����ϴ� ��ũ��Ʈ�Դϴ�.
{
    PlayerMove playerInfo;              // Player�� ������ �޾ƿɴϴ�.


    void Start()
    {
        playerInfo = FindObjectOfType<PlayerMove>();
    }

    void Update()
    {

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player") 
        {
            playerInfo.holdingTime = 0f;    // Player�� �����ϸ� PlayerMove ��ũ��Ʈ�� holdingTime(�����ð�)�� 0���� �ʱ�ȭ�մϴ�.
            playerInfo.moveSpeed *= 2f;     // �÷��̾��� �̵��ӵ��� 2��� ������ŵ�ϴ�.

            gameObject.SetActive(false);    // �̵��ӵ� ������ ������Ʈ�� ��Ȱ��ȭ �մϴ�.
        }
    }
}
