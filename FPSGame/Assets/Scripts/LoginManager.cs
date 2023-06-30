using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{

    public TMP_InputField id;                           //사용자 아이디
    public TMP_InputField password;         //사용자 패스워드

    public TextMeshProUGUI notify;          //검사 텍스트 변수


    private void Start()
    {
        notify.text = "";                   //검사 텍스트 창을 비운다.
    }

    public void SaveUserData()              //아이디와 패스워드 저장 함수.
    {
        if(!CheckInput(id.text , password.text)) return;

        //같은 아이디가 존재 하지않는다면
        if (!PlayerPrefs.HasKey(id.text))
        {
            //사용자 아이디는 key로  패스워드는 값
            PlayerPrefs.SetString(id.text, password.text);
            notify.text = "아이디 생성이 완료 되었습니다.";
        }
        else
        {
            notify.text = "이미 존재하는 아이디 입니다.";
        }
    }
    public void CheckUserData()
    {
        if (!CheckInput(id.text, password.text)) return;
        //저장되있는  패스워드값이
        string pass = PlayerPrefs.GetString(id.text);
        //사용자가 입력한  패스워드와 같다면 
        if (password.text == pass)
        {
            SceneManager.LoadScene(1);      //Main씬을 불러옴
        }
        else
        {
            notify.text = "입력하신 아이디와 패스워드가 일치하지 않습니다.";
        }
    }

    bool CheckInput(string id, string pwd)
    {
        //if (id =="" || pwd =="")
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pwd))
        {
            notify.text = "아이디 또는 패스워드를 입력해주세요";
            return false;
        }
        else return true;
    }
 

}
