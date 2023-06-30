using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 200f;
    float mx = 0f;
    float my = 0f;

    public Transform spine;

    public Vector3 ChestOffset;

    Vector3 ChestDir = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        spine = GameManager.Instance.player.anim.GetBoneTransform(HumanBodyBones.Spine);
    }

    // Update is called once per frame
    void Update()
    {
        //���� �� ���°� �ƴϸ� ���� ���� ����
        if (GameManager.Instance.gState != GameManager.GameState.Run) return;

        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");
        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;

        my = Mathf.Clamp(my, -30f, 30f);
        transform.eulerAngles = new Vector3(-my, mx, 0);
        //ī�޶� �����ִ� ����

      

    }

    private void LateUpdate()
    {
        ChestDir = Camera.main.transform.position + Camera.main.transform.forward * 50f;

       
        //  spine.LookAt(ChestDir); //��ü�� ī�޶� ���¹������� ����

        spine.rotation = spine.transform.rotation * Quaternion.Euler(0,0, -my);
    }

}
