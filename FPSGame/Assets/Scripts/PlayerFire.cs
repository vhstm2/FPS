using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 오른쪽 버튼을 누르는 시간에 따라 파워값을 조절해서 멀리 던지게 구현   (기모으기)
/// 각도에 따라서 날아가는 방향 구현
/// </summary>
/// 
public enum WeaponMode                             //무기 모드 
{
    Normal,
    Rifle,
    Sniper
}
public class PlayerFire : MonoBehaviour
{
    public GameObject[] eff_Flash;              //총 발사  효과 오브젝트 배열

    public TextMeshProUGUI wModeText;

    public GameObject firePosition;     //발사위치
    public GameObject bombFactory;    //수류탄 오브젝트
    public float throwPower = 5f;      //투척 파워

    public GameObject bulletEffect;     //피격 이펙트 옵젝트
    public ParticleSystem ps;          //피격 이펙트 파티클 시스템

    #region
    public int basicbullet = 30; //기본총알 30개
    public int sniperbullet = 10; //스나이퍼총알 10개
    public int bombbullet = 2; //수류탄 2개

    public int weaponPower = 5; // 일반 무기 데미지
    public int headPower = 30; // 헤드샷 데미지
    public int sniperPower = 20; //스나이퍼 데미지
    #endregion



    public bool isThrow = false;

    public AudioSource playerAudio;            //오디오 소스 컴포넌트

    //무기 아이콘 스프라이트이미지
    public GameObject weapon0;
    public GameObject weapon1;
    public GameObject weapon2;

    public GameObject crossHair01;
    public GameObject crossHair02;
    public GameObject crossHair02_zoom;                         //마우스 우클릭 줌모드 스프라이트

    //마우스 오른쪽 버튼 클릭 아이콘
    public GameObject weapon01_R;
    public GameObject weapon02_R;

    public Slider bombCollTimeSlider;


    
    public WeaponMode wMode;                   //무기모드 변수
    [NonSerialized]public bool ZoomMode = false;              //카메라 확대 확인용 변수

    public float bombTimer = 0;

    #region
    public bool useItem = false;        // 아이템 사용 여부 판단합니다.
    public int fieldItem_count = 0;     // Field 아이템 소지갯수를 저장합니다.
    public GameObject FieldItem;        // 무적 아이템 사용 시 플레이어 옆에 무적 유지 중임을 표시해줄 오브젝트용 입니다.
    public bool fieldItemUse = false;   // 무적아이템 사용 여부를 판단합니다.
    #endregion
    private void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
        wMode = WeaponMode.Normal;                  //무기모드를 노말모드로 설정

        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        //게임 중 생태가 아니면 전부 실행 Update문 실행종료

        #region
        if (Input.GetKeyUp(KeyCode.F))                              // F키를 누르면 무적 아이템을 사용할 수 있습니다.
        {
            if (fieldItem_count > 0 && fieldItemUse == false)       // 무적 아이템의 갯수가 0개보다 많고 현재 무적 아이템 사용중이 아니라면
            {
                fieldItemUse = true;                                // 무적 아이템 사용 여부를 True로 변경해줍니다.
                FieldItem.SetActive(true);                          // 무적임을 표시해주는 오브젝트 출력
                fieldItem_count--;                                  // 무적 아이템 사용 갯수를 1개 줄여줍니다.
            }
        }

        if (fieldItemUse == true)                                    // 무적이 유지 중이라면
        {
            PlayerMove invin = GetComponent<PlayerMove>();
            invin.hp = invin.dumyHp;                                // PlayerMove 컴포넌트에 있는 hp값을 dumyHp값으로 고정합니다.
        }
        #endregion


    }

    public  IEnumerator ShootEffectOn(float duration)           //총구 화염 효과  코루틴 
    {
        int num = UnityEngine.Random.Range(0, eff_Flash.Length);
        eff_Flash[num].SetActive(true);
        yield return new WaitForSeconds(duration);
        eff_Flash[num].SetActive(false);
        yield return null;
    }






}
