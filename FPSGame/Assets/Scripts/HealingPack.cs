using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class HealingPack : MonoBehaviour
{
    PlayerMove playerInfo;    // Player의 정보를 받아옵니다.

    void Start()
    {
        playerInfo = FindObjectOfType<PlayerMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
                if (playerInfo.hp >= playerInfo.maxHp)          // 플레이어 체력이 가득찼다면
                {
                    return;                                     // 실행하지 않습니다.
                }
                else if (playerInfo.maxHp - playerInfo.hp < 10) // 플레이어의 잃은 체력이 10 이하라면
                {
                    playerInfo.hp = playerInfo.maxHp;           // 현재체력을 풀체력으로 만듭니다.
                }
                else
                    playerInfo.hp += 10;                        // 체력을 10 회복합니다.

                gameObject.SetActive(false);                    // 체력회복 오브젝트를 비활성화합니다.
            }

        /*아래는 테스트용 코드입니다.*/

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
            if (gameObject.tag == "HealingPack")    // 사용한 아이템이 힐링팩이라면
            {
                if (playerInfo.hp >= playerInfo.maxHp)        // 플레이어 체력이 가득찼다면
                {
                    return;                             // 실행하지 않습니다.
                }
                else if (playerInfo.maxHp - playerInfo.hp < 10) // 플레이어의 잃은 체력이 10 이하라면
                {
                    playerInfo.hp = playerInfo.maxHp;       // 현재체력을 풀체력으로 만듭니다.
                }
                else
                    playerInfo.hp += 10;                  // 체력을 10 회복합니다.
            }

            if (gameObject.tag == "BoostPack")
            {
                playerInfo.moveSpeed *= 2;
            }
        }

        Destroy(gameObject);
    }*/