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
        //게임 중 생태가 아니면 전부 실행 금지
        if (GameManager.Instance.gState != GameManager.GameState.Run) return;

        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");
        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;

        my = Mathf.Clamp(my, -30f, 30f);
        transform.eulerAngles = new Vector3(-my, mx, 0);
        //카메라가 보고있는 방향

      

    }

    private void LateUpdate()
    {
        ChestDir = Camera.main.transform.position + Camera.main.transform.forward * 50f;

       
        //  spine.LookAt(ChestDir); //상체를 카메라 보는방향으로 보기

        spine.rotation = spine.transform.rotation * Quaternion.Euler(0,0, -my);
    }

}
