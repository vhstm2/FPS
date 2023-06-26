using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
        public GameObject bombEffect;       //폭팔 이펙트 프리팹
        private void OnCollisionEnter(Collision collision)
        {
                GameObject eff = Instantiate(bombEffect);   //이팩트 프리팹 생성
                eff.transform.position = transform.position;     //수류탄 위치와 이펙트 위치를 동일하게 설정
                Destroy(gameObject);                            //충돌 했을때 본인 제거
        }


}
