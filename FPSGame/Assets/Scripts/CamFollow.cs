using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
   
    // Update is called once per frame
    void Update()
    {
        //메인카메라의 위치를 Camposition의 위치와 동일하게 한다.
        transform.position = target.position;
    }
}
