using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{

    public TMP_InputField id;                           //����� ���̵�
    public TMP_InputField password;         //����� �н�����

    public TextMeshProUGUI notify;          //�˻� �ؽ�Ʈ ����


    private void Start()
    {
        notify.text = "";                   //�˻� �ؽ�Ʈ â�� ����.
    }

    public void SaveUserData()              //���̵�� �н����� ���� �Լ�.
    {
        if(!CheckInput(id.text , password.text)) return;

        //���� ���̵� ���� �����ʴ´ٸ�
        if (!PlayerPrefs.HasKey(id.text))
        {
            //����� ���̵�� key��  �н������ ��
            PlayerPrefs.SetString(id.text, password.text);
            notify.text = "���̵� ������ �Ϸ� �Ǿ����ϴ�.";
        }
        else
        {
            notify.text = "�̹� �����ϴ� ���̵� �Դϴ�.";
        }
    }
    public void CheckUserData()
    {
        if (!CheckInput(id.text, password.text)) return;
        //������ִ�  �н����尪��
        string pass = PlayerPrefs.GetString(id.text);
        //����ڰ� �Է���  �н������ ���ٸ� 
        if (password.text == pass)
        {
            SceneManager.LoadScene(1);      //Main���� �ҷ���
        }
        else
        {
            notify.text = "�Է��Ͻ� ���̵�� �н����尡 ��ġ���� �ʽ��ϴ�.";
        }
    }

    bool CheckInput(string id, string pwd)
    {
        //if (id =="" || pwd =="")
        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pwd))
        {
            notify.text = "���̵� �Ǵ� �н����带 �Է����ּ���";
            return false;
        }
        else return true;
    }
 

}
