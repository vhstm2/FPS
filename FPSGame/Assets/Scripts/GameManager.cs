using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using System;
using UnityEngine.SceneManagement;

[Serializable]
public struct soundClip
{
    public string name;
    public AudioClip clip;
}

public class GameManager : Singleton<GameManager>
{
    public AudioSource source;
    public soundClip[] clip;

    public GameObject gameLabel;                    // 게임 상태 UI 오브젝트 변수

    TextMeshProUGUI gameText;

     public PlayerMove player;

    public enum GameState                               //게임 상태 상수 (열거형)
    {
        Ready, Run, Pause,GameOver
    }
    public GameState gState;                        //현재의 게임 상태 변수

    public GameObject gameOption;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMove>();                                             //PlayerMove 스크립트를 가지고있는 player를 가져온다.
    }
    private void Start()
    {
        gState = GameState.Ready;                                                                               //초기 게임 상태는 준비 상태로 설정
        gameText = gameLabel.GetComponent<TextMeshProUGUI>();       //게임 상태 UI 오브젝트에서 TextMeshProUGUI 컴포넌트를 가져온다.
        gameText.text = "Ready...";                                                                               //게임 상태 텍스트를 Ready... 으로 설정
        gameText.color = new Color32(255, 185, 0, 255);                                         //게임 상태 텍스트의  주황색으로 변경 

        //마우스 커서를 ESC 누리기전까지 잠궈둔다.
        //Cursor.lockState = CursorLockMode.Locked;               //마우스를 중앙 고정
        //마우스 커서를 윈도우 밖으로 나가지 않게 함
        //Cursor.lockState = CursorLockMode.Confined;


        StartCoroutine(nameof(ReadyToStart));                                                 //게임 준비 -> 게임 중 상태로 변경
    }

   

    IEnumerator ReadyToStart()
    {
        yield return new WaitForSeconds(2f);                                                        //2초간 대기
        gameText.text = "Go!";                                                                                         //상태 텍스트의 내용을 GO! 로 변경
        yield return new WaitForSeconds(0.5f);                                                    //0.5초간 대기
        gameLabel.SetActive(false);                                                                             //상태 텍스트를 비활성화
        gState = GameState.Run;                                                                                   //"게임 중 " 상태로 변경

        source.clip = clip[0].clip;
        source.Play();
    }


    private void Update()
    {
        //ESC
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Cursor.lockState = CursorLockMode.None;                     //마우스 잠금을 풀고 
            openOptionWindow();                                                                 //옵션창을 연다.
        }

        //플레이어가 죽었을때  후보2
        if(player.hp <= 0) 
        {
            gameLabel.SetActive(true);                                                                      //상태 텍스트 활성화
            gameText.text = "Game Over";                                                                //상태 텍스트 GameOver 출력
            gameText.color = new Color32(255, 0, 0, 255);                                   //상태 텍스트 색상을 Red로 설정
            gState = GameState.GameOver;                                                            //상태를 GameOver 로 변경
        }
    }

    #region 옵션패널 켜기
    public void openOptionWindow()          //옵션화면 켜기
    {
        gameOption.SetActive(true);         //옵션창을 활성화 
        Time.timeScale = 0;                                 //게임속도를 0배속으로 전환
        gState = GameState.Pause;               //게임상태를 일시정지로 변경
    }
    #endregion

    #region 옵션패널 끄기
    public void CloseOptionWindow()         //옵션화면 끄기
    {
        Cursor.lockState = CursorLockMode.Locked;           //계속하기 눌렀을때 마우스를 잠군다.
        gameOption.SetActive(false);        //옵션창을 비활성화
        Time.timeScale = 1;                                 //게임속도를 1배속으로 전환
        gState = GameState.Run;                 //게임상태를 플레이중으로 전환
    }
    #endregion

    #region 다시시작
    public void ReStartGame()               //다시하기 버튼
    {
        Time.timeScale = 1;                         //게임속도를 1배속으로 전환
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       //현재씬의 번호로 다시 로드.
    }
    #endregion

    #region 게임종료
    public void QuitGame()              //종료하기 버튼
    {
        Application.Quit();                 //게임을 종료한다. (빌드후 작동)
    }
    #endregion

}
