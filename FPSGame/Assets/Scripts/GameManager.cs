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

    public GameObject gameLabel;                    // ���� ���� UI ������Ʈ ����

    TextMeshProUGUI gameText;

     public PlayerMove player;

    public enum GameState                               //���� ���� ��� (������)
    {
        Ready, Run, Pause,GameOver
    }
    public GameState gState;                        //������ ���� ���� ����

    public GameObject gameOption;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMove>();                                             //PlayerMove ��ũ��Ʈ�� �������ִ� player�� �����´�.
    }
    private void Start()
    {
        gState = GameState.Ready;                                                                               //�ʱ� ���� ���´� �غ� ���·� ����
        gameText = gameLabel.GetComponent<TextMeshProUGUI>();       //���� ���� UI ������Ʈ���� TextMeshProUGUI ������Ʈ�� �����´�.
        gameText.text = "Ready...";                                                                               //���� ���� �ؽ�Ʈ�� Ready... ���� ����
        gameText.color = new Color32(255, 185, 0, 255);                                         //���� ���� �ؽ�Ʈ��  ��Ȳ������ ���� 

        //���콺 Ŀ���� ESC ������������ ��ŵд�.
        //Cursor.lockState = CursorLockMode.Locked;               //���콺�� �߾� ����
        //���콺 Ŀ���� ������ ������ ������ �ʰ� ��
        //Cursor.lockState = CursorLockMode.Confined;


        StartCoroutine(nameof(ReadyToStart));                                                 //���� �غ� -> ���� �� ���·� ����
    }

   

    IEnumerator ReadyToStart()
    {
        yield return new WaitForSeconds(2f);                                                        //2�ʰ� ���
        gameText.text = "Go!";                                                                                         //���� �ؽ�Ʈ�� ������ GO! �� ����
        yield return new WaitForSeconds(0.5f);                                                    //0.5�ʰ� ���
        gameLabel.SetActive(false);                                                                             //���� �ؽ�Ʈ�� ��Ȱ��ȭ
        gState = GameState.Run;                                                                                   //"���� �� " ���·� ����

        source.clip = clip[0].clip;
        source.Play();
    }


    private void Update()
    {
        //ESC
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            Cursor.lockState = CursorLockMode.None;                     //���콺 ����� Ǯ�� 
            openOptionWindow();                                                                 //�ɼ�â�� ����.
        }

        //�÷��̾ �׾�����  �ĺ�2
        if(player.hp <= 0) 
        {
            gameLabel.SetActive(true);                                                                      //���� �ؽ�Ʈ Ȱ��ȭ
            gameText.text = "Game Over";                                                                //���� �ؽ�Ʈ GameOver ���
            gameText.color = new Color32(255, 0, 0, 255);                                   //���� �ؽ�Ʈ ������ Red�� ����
            gState = GameState.GameOver;                                                            //���¸� GameOver �� ����
        }
    }

    #region �ɼ��г� �ѱ�
    public void openOptionWindow()          //�ɼ�ȭ�� �ѱ�
    {
        gameOption.SetActive(true);         //�ɼ�â�� Ȱ��ȭ 
        Time.timeScale = 0;                                 //���Ӽӵ��� 0������� ��ȯ
        gState = GameState.Pause;               //���ӻ��¸� �Ͻ������� ����
    }
    #endregion

    #region �ɼ��г� ����
    public void CloseOptionWindow()         //�ɼ�ȭ�� ����
    {
        Cursor.lockState = CursorLockMode.Locked;           //����ϱ� �������� ���콺�� �ᱺ��.
        gameOption.SetActive(false);        //�ɼ�â�� ��Ȱ��ȭ
        Time.timeScale = 1;                                 //���Ӽӵ��� 1������� ��ȯ
        gState = GameState.Run;                 //���ӻ��¸� �÷��������� ��ȯ
    }
    #endregion

    #region �ٽý���
    public void ReStartGame()               //�ٽ��ϱ� ��ư
    {
        Time.timeScale = 1;                         //���Ӽӵ��� 1������� ��ȯ
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       //������� ��ȣ�� �ٽ� �ε�.
    }
    #endregion

    #region ��������
    public void QuitGame()              //�����ϱ� ��ư
    {
        Application.Quit();                 //������ �����Ѵ�. (������ �۵�)
    }
    #endregion

}
