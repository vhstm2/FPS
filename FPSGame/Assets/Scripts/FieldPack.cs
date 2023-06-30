using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPack : MonoBehaviour  // 무적 아이템을 구현하는 스크립트입니다.
{
    PlayerFire packCount;               // PlayerFire 스크립트에 있는 무적 아이템 소지갯수에 관여합니다.

    void Start()
    {
        packCount=FindObjectOfType<PlayerFire>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Player")
        {
            packCount.fieldItem_count++;    // PlayerFire 스크립트의 무적 아이템 소지갯수를 1 증가시켜 줍니다.

            Destroy(gameObject);            // 무적 오브젝트를 파괴합니다.
        }
    }
}
