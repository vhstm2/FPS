using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;                //폭팔 이펙트 프리팹

    public int attackPower = 10;                          //수류탄 파워

    public float explosionRadius = 5f;              //수류탄 폭팔 반경

    // public GameObject bombTarget;

    [SerializeField]  private AudioSource bombAudio;            //오디오 소스 컴포넌트
    private void OnCollisionEnter(Collision collision)
    {
        bombAudio.PlayOneShot(bombAudio.clip);

        //overlapSphere : 특정 좌표를 기준으로 반경안에 있는 오브젝트의 컬라이더 정보를 배열로 받아온다.
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 10);
        //폭팍 효과 범위안에 있는 에너미 오브젝트들에게 데미지를 입힘.
        foreach (Collider col in cols)
        {
            col.GetComponent<EnemyFSM>().HitEnemy(attackPower);
        }

        GameObject eff = Instantiate(bombEffect);                                                               //이팩트 프리팹 생성
        eff.transform.position = transform.position;                                                            //수류탄 위치와 이펙트 위치를 동일하게 설정

        transform.GetChild(0).gameObject.SetActive(false);                                          //수류탄의  자식 모델에 컬라이더 추가 후 비활성화 
        Destroy(gameObject , bombAudio.clip.samples);                                                       //충돌 했을때  사운드길이 만큼 기다린 후 본인 제거 
    }
}
