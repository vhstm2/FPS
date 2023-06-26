using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float destroyTime = 1.5f;
    private float currentTime = 0;


    void Update()
    {
        //1.5 �� �ڿ� �ڱ� �ڽ��� ����
        if(currentTime > destroyTime) Destroy(gameObject);
        currentTime += Time.deltaTime;      //�ð��� ���.
    }
}
