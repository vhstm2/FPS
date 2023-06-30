using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���׸� �̱���
/// </summary>
/// <typeparam name="T"></typeparam>
public class  Singleton<T> : MonoBehaviour  where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    { 
        get 
        {
            // ���ӿ���Ʈ ������ ã�ƺ��� �̱����� �پ��ִ� �༮�� �̸����� 
            GameObject obj;
            obj = GameObject.Find(typeof(T).Name);
            //ã�ƺôµ� ���ٸ� 
            if ((obj == null))
            {
                //���ӿ�����Ʈ�� ���� ����
                GameObject g = new GameObject();
                //�̱��� ������ �ٿ��ش�.
                instance = g.AddComponent<T>();
                
            }
            else
            { 
                //�ִٸ� ã�Ƽ� �־��ش�.
                instance = obj.GetComponent<T>();
            }
            return instance; 
        }
    }
}
