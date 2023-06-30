using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMachine : MonoBehaviour
{
    public WeaponMode type = WeaponMode.Normal;

    public GameObject sword;
    public GameObject rifle;
    public GameObject sniper;

    private Weapon weaponBase;

    public PlayerFire player;

    private void Awake()
    {
        player = GetComponent<PlayerFire>();
    }

    public void EquipBat()
    {
        type = WeaponMode.Normal;
        sword.SetActive(true);
        rifle.SetActive(false);
        sniper.SetActive(false);
    }

    public void EquipRifle()
    {
        type = WeaponMode.Rifle;
        sword.SetActive(false);
        rifle.SetActive(true);
        sniper.SetActive(false);
    }

    public void EquipSniper()
    {
        type = WeaponMode.Sniper;
        sword.SetActive(false);
        rifle.SetActive(false);
        sniper.SetActive(true);
    }

    private void Start()
    {
        StateChange(WeaponMode.Normal);
    }

    private void Update()
    {
        if (GameManager.Instance.gState != GameManager.GameState.Run) return;
        if (weaponBase != null) weaponBase.OnUpdate();

        if (Input.GetKeyDown(KeyCode.Z)) StateChange(WeaponMode.Normal);

        if (Input.GetKeyDown(KeyCode.X)) StateChange(WeaponMode.Rifle);

        if (Input.GetKeyDown(KeyCode.C)) StateChange(WeaponMode.Sniper);
    }


    public void StateChange(WeaponMode type)
    {
        if (weaponBase != null) weaponBase.End();

        weaponBase = InstanceWeapon(type);
        this.type = type;
        weaponBase.Init();
    }

    private Weapon InstanceWeapon(WeaponMode type)
    {
        switch (type)
        {
            case WeaponMode.Normal: return new SWORD(this);
            case WeaponMode.Rifle: return new RIFLE(this);
            case WeaponMode.Sniper: return new SNIPER(this);
        }
        return null;
    }
}


public class Weapon
{
   
    protected WeaponMachine weaponInfo;
    public Weapon(WeaponMachine _weaponChange)
    {
        weaponInfo = _weaponChange;
    }

    public virtual void Init() { }

    public virtual void OnUpdate() 
    {
        GunProcess();
    }

    public virtual void End() { }


    public virtual void GunProcess()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //���̸� ������ �� �߻�� ��ġ�� ���� ���� ����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = new RaycastHit();     //���̰� �΋H�� ������Ʈ�� ������ �����´�.

            weaponInfo.player.playerAudio.Play();

            //���̸� �߻��� �� ���� �΋H�� ��ü�� ������ ����Ʈ ǥ��
            if (Physics.Raycast(ray, out hitInfo))
            {
                //�΋H�� ��ü�� �ݶ��̴��� ������
                if (hitInfo.collider != null)
                {
                    //if (hitInfo.collider.TryGetComponent<EnemyFSM>(out EnemyFSM enemy))
                    //    enemy.HitEnemy(weaponPower);

                    //���̰� ����  ������Ʈ�� Enemy���
                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        //���ʹ� ��ũ��Ʈ�� HitEnemy �Լ��� �����Ͽ� �÷��̾��� ������ ����
                        EnemyFSM eFSM = hitInfo.collider.gameObject.GetComponent<EnemyFSM>();
                        eFSM.HitEnemy(weaponInfo.player.weaponPower);
                        eFSM.BloodEffect(hitInfo.point, hitInfo.normal);
                    }

                    if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Head"))
                    {
                        var enemy = hitInfo.collider.gameObject.GetComponentInParent<EnemyFSM>();
                        enemy.HitEnemy(enemy.hp);

                        enemy.HeadShoot();
                        //��弦 �̹��� ��� 
                    }


                    //�΋H�� �������� �ǰ� ����Ʈ ��ġ ���� ����
                    weaponInfo.player.bulletEffect.transform.position = hitInfo.point;
                    weaponInfo.player.bulletEffect.transform.forward = hitInfo.normal;
                    weaponInfo.player.ps.Play();
                }
                weaponInfo.StartCoroutine(weaponInfo.player.ShootEffectOn(0.05f));
            }

        }
    }

}

public class SWORD : Weapon
{
    public SWORD(WeaponMachine _weaponChange) : base(_weaponChange)
    { }

    public override void Init()
    {
        weaponInfo.EquipBat();

        GameManager.Instance.player.anim.SetLayerWeight(1, 0);
        GameManager.Instance.player.anim.SetLayerWeight(2, 0);
        weaponInfo.player.wModeText.text = "Sword Mode";                         //text �� Normal Mode �� ����

        weaponInfo.player.weapon0.SetActive(true);
        weaponInfo.player.weapon1.SetActive(false);
        weaponInfo.player.weapon2.SetActive(false);
        weaponInfo.player.crossHair01.SetActive(false);
        weaponInfo.player.crossHair02.SetActive(false);
    }

    public override void OnUpdate()
    {
       
    }

    public override void End()
    {
        base.End();
    }

}

public class RIFLE : Weapon
{

    public RIFLE(WeaponMachine _weaponChange) : base(_weaponChange)
    { }

    public override void Init()
    {
        weaponInfo.EquipRifle();
        GameManager.Instance.player.anim.SetLayerWeight(1, 1);
        GameManager.Instance.player.anim.SetLayerWeight(2, 0);

        weaponInfo.player.wMode = WeaponMode.Normal;                                  //�����带 Normal Mode�� ���� 
        Camera.main.fieldOfView = 60;                                                                           // ȭ�� �þ߰��� �������  60���� ����.
        weaponInfo.player.wModeText.text = "Rifle Mode";                         //text �� Normal Mode �� ����
        weaponInfo.player.weapon0.SetActive(false);
        weaponInfo.player.weapon1.SetActive(true);
        weaponInfo.player.weapon01_R.SetActive(true);
        weaponInfo.player.crossHair01.SetActive(true);

        weaponInfo.player.weapon2.SetActive(false);
        weaponInfo.player.weapon02_R.SetActive(false);
        weaponInfo.player.crossHair02.SetActive(false);

        weaponInfo.player.crossHair02_zoom.SetActive(false);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKeyDown(KeyCode.R)) //������
        {
            weaponInfo.player.basicbullet = 30;
        }


            if (Input.GetMouseButtonUp(1))
        {
            if (weaponInfo.player.isThrow)
            {
                //����ź ������Ʈ ���� �� ����ź�� ������ġ�� �߻���ġ�� ����
                GameObject bomb = GameObject.Instantiate(weaponInfo.player.bombFactory);
                bomb.transform.position = weaponInfo.player.firePosition.transform.position;

                Rigidbody rb = bomb.GetComponent<Rigidbody>();

                Vector3 r = new Vector3(UnityEngine.Random.Range(-90, 90), UnityEngine.Random.Range(-90, 90), UnityEngine.Random.Range(-90, 90));
                rb.AddForce(Camera.main.transform.forward * weaponInfo.player.throwPower, ForceMode.Impulse);
                rb.AddTorque(r * weaponInfo.player.throwPower, ForceMode.Impulse);
                weaponInfo.player.bombTimer = 0;
                weaponInfo.player. isThrow = false;
                weaponInfo.player.throwPower = 5;
            }
        }
        if (Input.GetMouseButton(1) && weaponInfo.player.wMode == WeaponMode.Normal)
        {
            weaponInfo.player.throwPower += Time.deltaTime * 10f;
            //throwPower �ִ� 20���� ����
            weaponInfo.player.throwPower = Mathf.Clamp(weaponInfo.player.throwPower, 0, 20);
        }

        float maxBombTime = 1f;

        weaponInfo.player.bombCollTimeSlider.value = weaponInfo.player.bombTimer / maxBombTime;
        if ((weaponInfo.player.bombTimer += Time.deltaTime) >= maxBombTime && !weaponInfo.player.isThrow)
        {
            weaponInfo.player.isThrow = true;
        }

    }

    public override void End()
    {
        base.End();
    }
}

public class SNIPER : Weapon
{
    public SNIPER(WeaponMachine _weaponChange) : base(_weaponChange)
    { }
    public override void Init()
    {
        weaponInfo.EquipSniper();
        GameManager.Instance.player.anim.SetLayerWeight(1, 1);
        GameManager.Instance.player.anim.SetLayerWeight(2, 0);

        weaponInfo.player.wMode = WeaponMode.Sniper;                                 //�����带 Sniper�� ����
        weaponInfo.player.wModeText.text = "Sniper Mode";                         //text�� Sniper Mode �� ����
        weaponInfo.player.weapon0.SetActive(false);
        weaponInfo.player.weapon1.SetActive(false);
        weaponInfo.player.weapon01_R.SetActive(false);
        weaponInfo.player.crossHair01.SetActive(false);

        weaponInfo.player.weapon2.SetActive(true);
        weaponInfo.player.weapon02_R.SetActive(true);
        weaponInfo.player.crossHair02.SetActive(true);
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKeyDown(KeyCode.R)) //������
        {
            weaponInfo.player.sniperbullet = 10;
        }


        if (Input.GetMouseButtonDown(1))
        {
            if (weaponInfo.player.ZoomMode == false)          //�� ��� ���°� �ƴ϶�� ī�޶� Ȯ�� ��Ű��  �� ��� ���·� ����
            {
                Camera.main.fieldOfView = 15;
                weaponInfo.player.ZoomMode = true;
                weaponInfo.player.crossHair02_zoom.SetActive(true);
                weaponInfo.player.crossHair02.SetActive(false);
            }
            else
            {
                Camera.main.fieldOfView = 60;
                weaponInfo.player.ZoomMode = false;
                weaponInfo.player.crossHair02_zoom.SetActive(false);
                weaponInfo.player.crossHair02.SetActive(true);
            }
        }

    }

    public override void End()
    {
        base.End();
    }
}