using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class HealingPack : MonoBehaviour
{
    PlayerMove playerInfo;    // Player�� ������ �޾ƿɴϴ�.

    void Start()
    {
        playerInfo = FindObjectOfType<PlayerMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
                if (playerInfo.hp >= playerInfo.maxHp)          // �÷��̾� ü���� ����á�ٸ�
                {
                    return;                                     // �������� �ʽ��ϴ�.
                }
                else if (playerInfo.maxHp - playerInfo.hp < 10) // �÷��̾��� ���� ü���� 10 ���϶��
                {
                    playerInfo.hp = playerInfo.maxHp;           // ����ü���� Ǯü������ ����ϴ�.
                }
                else
                    playerInfo.hp += 10;                        // ü���� 10 ȸ���մϴ�.

                gameObject.SetActive(false);                    // ü��ȸ�� ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            }

        /*�Ʒ��� �׽�Ʈ�� �ڵ��Դϴ�.*/

            /*else if (other.tag == "BoostPack")
            {
                holdingTime = 0;

                playerInfo.moveSpeed += 7f;

                if(holdingTime > durationTime)
                {
                    playerInfo.moveSpeed = 7f;
                }

                BoostPack.SetActive(false);
            }*/
        }
    }

    /*public void checkUsedItem(bool usedItem)
    {
        if (usedItem)
        {
            if (gameObject.tag == "HealingPack")    // ����� �������� �������̶��
            {
                if (playerInfo.hp >= playerInfo.maxHp)        // �÷��̾� ü���� ����á�ٸ�
                {
                    return;                             // �������� �ʽ��ϴ�.
                }
                else if (playerInfo.maxHp - playerInfo.hp < 10) // �÷��̾��� ���� ü���� 10 ���϶��
                {
                    playerInfo.hp = playerInfo.maxHp;       // ����ü���� Ǯü������ ����ϴ�.
                }
                else
                    playerInfo.hp += 10;                  // ü���� 10 ȸ���մϴ�.
            }

            if (gameObject.tag == "BoostPack")
            {
                playerInfo.moveSpeed *= 2;
            }
        }

        Destroy(gameObject);
    }*/