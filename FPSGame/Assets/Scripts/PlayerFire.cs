using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ������ ��ư�� ������ �ð��� ���� �Ŀ����� �����ؼ� �ָ� ������ ����   (�������)
/// ������ ���� ���ư��� ���� ����
/// </summary>
/// 
public enum WeaponMode                             //���� ��� 
{
    Normal,
    Rifle,
    Sniper
}
public class PlayerFire : MonoBehaviour
{
    public GameObject[] eff_Flash;              //�� �߻�  ȿ�� ������Ʈ �迭

    public TextMeshProUGUI wModeText;

    public GameObject firePosition;     //�߻���ġ
    public GameObject bombFactory;    //����ź ������Ʈ
    public float throwPower = 5f;      //��ô �Ŀ�

    public GameObject bulletEffect;     //�ǰ� ����Ʈ ����Ʈ
    public ParticleSystem ps;          //�ǰ� ����Ʈ ��ƼŬ �ý���

    #region
    public int basicbullet = 30; //�⺻�Ѿ� 30��
    public int sniperbullet = 10; //���������Ѿ� 10��
    public int bombbullet = 2; //����ź 2��

    public int weaponPower = 5; // �Ϲ� ���� ������
    public int headPower = 30; // ��弦 ������
    public int sniperPower = 20; //�������� ������
    #endregion



    public bool isThrow = false;

    public AudioSource playerAudio;            //����� �ҽ� ������Ʈ

    //���� ������ ��������Ʈ�̹���
    public GameObject weapon0;
    public GameObject weapon1;
    public GameObject weapon2;

    public GameObject crossHair01;
    public GameObject crossHair02;
    public GameObject crossHair02_zoom;                         //���콺 ��Ŭ�� �ܸ�� ��������Ʈ

    //���콺 ������ ��ư Ŭ�� ������
    public GameObject weapon01_R;
    public GameObject weapon02_R;

    public Slider bombCollTimeSlider;


    
    public WeaponMode wMode;                   //������ ����
    [NonSerialized]public bool ZoomMode = false;              //ī�޶� Ȯ�� Ȯ�ο� ����

    public float bombTimer = 0;

    #region
    public bool useItem = false;        // ������ ��� ���� �Ǵ��մϴ�.
    public int fieldItem_count = 0;     // Field ������ ���������� �����մϴ�.
    public GameObject FieldItem;        // ���� ������ ��� �� �÷��̾� ���� ���� ���� ������ ǥ������ ������Ʈ�� �Դϴ�.
    public bool fieldItemUse = false;   // ���������� ��� ���θ� �Ǵ��մϴ�.
    #endregion
    private void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
        wMode = WeaponMode.Normal;                  //�����带 �븻���� ����

        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        //���� �� ���°� �ƴϸ� ���� ���� Update�� ��������

        #region
        if (Input.GetKeyUp(KeyCode.F))                              // FŰ�� ������ ���� �������� ����� �� �ֽ��ϴ�.
        {
            if (fieldItem_count > 0 && fieldItemUse == false)       // ���� �������� ������ 0������ ���� ���� ���� ������ ������� �ƴ϶��
            {
                fieldItemUse = true;                                // ���� ������ ��� ���θ� True�� �������ݴϴ�.
                FieldItem.SetActive(true);                          // �������� ǥ�����ִ� ������Ʈ ���
                fieldItem_count--;                                  // ���� ������ ��� ������ 1�� �ٿ��ݴϴ�.
            }
        }

        if (fieldItemUse == true)                                    // ������ ���� ���̶��
        {
            PlayerMove invin = GetComponent<PlayerMove>();
            invin.hp = invin.dumyHp;                                // PlayerMove ������Ʈ�� �ִ� hp���� dumyHp������ �����մϴ�.
        }
        #endregion


    }

    public  IEnumerator ShootEffectOn(float duration)           //�ѱ� ȭ�� ȿ��  �ڷ�ƾ 
    {
        int num = UnityEngine.Random.Range(0, eff_Flash.Length);
        eff_Flash[num].SetActive(true);
        yield return new WaitForSeconds(duration);
        eff_Flash[num].SetActive(false);
        yield return null;
    }






}
