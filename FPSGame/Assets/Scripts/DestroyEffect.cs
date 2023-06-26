using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float destroyTime = 1.5f;
    private float currentTime = 0;


    void Update()
    {
        //1.5 초 뒤에 자기 자신을 제거
        if(currentTime > destroyTime) Destroy(gameObject);
        currentTime += Time.deltaTime;      //시간을 잰다.
    }
}
