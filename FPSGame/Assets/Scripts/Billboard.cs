using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform target;


    private void Awake()
    {
        target = GameManager.Instance.player.transform;
    }
    private void Update()
    {
        //�ڽ��� ������  ī�޶��� ����� ��ġ ��Ų��.
        transform.forward = target.forward;
    }
}
