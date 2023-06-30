using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LodingNextScene : MonoBehaviour
{
    public int sceneNumber = 2;         //���� �� �� ��ȣ
    public Slider loadingBar;               //�ε� �����̴�
    public TextMeshProUGUI loadingText;     //�ε� �ؽ�Ʈ


    private void Start()
    {
        StartCoroutine(TransitionNextScene(sceneNumber));
    }

    IEnumerator TransitionNextScene(int num)
    {
        //������ ���� �񵿱� �������� �ε��Ѵ�. 
        AsyncOperation ao = SceneManager.LoadSceneAsync(num);
        //�ε�Ǵ� ���� ����� ������ �ʰ� �Ѵ�.
        ao.allowSceneActivation = false;

        //�ε��� �Ϸᰡ �ɶ����� �ݺ��� ���� ��ҵ��� �ε��ϰ� ���� ������ ȭ�鿡 ǥ��
        while(!ao.isDone) 
        {
            //�ε� ������� �����̴��ٿ� �ؽ�Ʈ�� ǥ���Ѵ�.
            loadingBar.value = ao.progress;
            loadingText.text = (ao.progress * 100f).ToString() + "%";
            if (ao.progress >= 0.9f)    //���� �� �ε� ������� 90%�� �ѱ�� 
            {
                //�ε�� ���� ȭ�鿡 ���̰� �Ѵ�.
                ao.allowSceneActivation = true;           
            }
            yield return null;              //���� �������� �ɶ����� ���
        }

    }
}
