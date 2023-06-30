using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LodingNextScene : MonoBehaviour
{
    public int sceneNumber = 2;         //진행 할 씬 번호
    public Slider loadingBar;               //로딩 슬라이더
    public TextMeshProUGUI loadingText;     //로딩 텍스트


    private void Start()
    {
        StartCoroutine(TransitionNextScene(sceneNumber));
    }

    IEnumerator TransitionNextScene(int num)
    {
        //지정된 씬을 비동기 형식으로 로드한다. 
        AsyncOperation ao = SceneManager.LoadSceneAsync(num);
        //로드되는 씬의 모습을 보이지 않게 한다.
        ao.allowSceneActivation = false;

        //로딩이 완료가 될때까지 반복해 씬의 요소들을 로드하고 진행 과정을 화면에 표시
        while(!ao.isDone) 
        {
            //로드 진행률을 슬라이더바에 텍스트로 표시한다.
            loadingBar.value = ao.progress;
            loadingText.text = (ao.progress * 100f).ToString() + "%";
            if (ao.progress >= 0.9f)    //만일 씬 로드 진행률이 90%를 넘기면 
            {
                //로드된 씬을 화면에 보이게 한다.
                ao.allowSceneActivation = true;           
            }
            yield return null;              //다음 프레임이 될때까지 대기
        }

    }
}
