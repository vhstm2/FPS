using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject firePosition;     //발사위치
    public GameObject bombFactory;    //수류탄 오브젝트
    public float throwPower = 15f;      //투척 파워

    public GameObject bulletEffect;     //피격 이펙트 옵젝트
    private ParticleSystem ps;          //피격 이펙트 파티클 시스템

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
        if (Input.GetMouseButtonDown(1))    //오른쪽 마우스 버튼이 눌렸을때 
        { 
            //수류탄 오브젝트 생성 후 수류탄의 생성위치를 발사위치로 설정
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;

            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            //카메라의 정면 방향으로 수류탄에 물리적인 힘을 가한다. 

            //ForceMode
            //1. Porce  : 연속적인 힘 질량의 영향 0
            //2. Impulse : 순간적인 힘 , 질량의 영향 0
            //3, VelocityChange : 순간적인 힘 , 질량의 영향 X
            //4. Accleration : 연속적인 힘 , 질량의 영향 X

            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }

        if(Input.GetMouseButtonDown (0))
        {
            //레이를 생성한 후 발사된 위치와 진행 방향 설정
            Ray ray = new Ray(Camera.main.transform.position , Camera.main.transform.forward);
            RaycastHit hitInfo  = new RaycastHit();     //레이가 부딫힌 오브젝트의 정보를 가져온다.

            lr.SetPosition(0, ray.origin);

            lr.enabled = true;
            //레이를 발사한 후 만일 부딫힌 물체가 있으면 이펙트 표시
            if (Physics.Raycast(ray, out hitInfo))
            {
                //부딫힌 물체의 콜라이더가 있으면
                if (hitInfo.collider != null)
                {
                    //if (hitInfo.collider.TryGetComponent<EnemyFSM>(out EnemyFSM enemy))
                    //    enemy.HitEnemy(weaponPower);

                    //레이와 부딫힌 오브젝트가 Enemy라면
                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        //에너미 스크립트의 HitEnemy 함수를 실행하여 플레이어의 공격을 전달
                        EnemyFSM eFSM = hitInfo.collider.gameObject.GetComponent<EnemyFSM>();
                        eFSM.HitEnemy(weaponPower);
                    }
                    //부딫힌 지점으로 피격 이펙트 위치 방향 설정
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
