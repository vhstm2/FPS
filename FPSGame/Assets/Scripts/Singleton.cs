using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 제네릭 싱글톤
/// </summary>
/// <typeparam name="T"></typeparam>
public class  Singleton<T> : MonoBehaviour  where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    { 
        get 
        {
            // 게임오젝트 변수에 찾아본다 싱글톤이 붙어있는 녀석의 이름으로 
            GameObject obj;
            obj = GameObject.Find(typeof(T).Name);
            //찾아봤는데 없다면 
            if ((obj == null))
            {
                //게임오브젝트를 새로 생성
                GameObject g = new GameObject();
                //싱글톤 정보를 붙여준다.
                instance = g.AddComponent<T>();
                
            }
            else
            { 
                //있다면 찾아서 넣어준다.
                instance = obj.GetComponent<T>();
            }
            return instance; 
        }
    }
}
