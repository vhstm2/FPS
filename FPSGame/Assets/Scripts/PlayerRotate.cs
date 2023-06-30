using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotSpeed = 200f;
    float mx = 0f;
    

    // Update is called once per frame
    void Update()
    {
        //게임 중 생태가 아니면 전부 실행 금지
        if (GameManager.Instance.gState != GameManager.GameState.Run) return;

        float mouse_X = Input.GetAxis("Mouse X");
        
        mx += mouse_X * rotSpeed * Time.deltaTime;
       
        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
