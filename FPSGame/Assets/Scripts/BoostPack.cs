using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPack : MonoBehaviour  // 이동속도 증가 아이템을 구현하는 스크립트입니다.
{
    PlayerMove playerInfo;              // Player의 정보를 받아옵니다.


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
            playerInfo.holdingTime = 0f;    // Player와 접촉하면 PlayerMove 스크립트에 holdingTime(유지시간)을 0으로 초기화합니다.
            playerInfo.moveSpeed *= 2f;     // 플레이어의 이동속도를 2배로 증가시킵니다.

            gameObject.SetActive(false);    // 이동속도 증가용 오브젝트를 비활성화 합니다.
        }
    }
}
