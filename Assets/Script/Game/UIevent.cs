using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIevent : MonoBehaviour
{
    private bool pauseOn = false;
    private GameObject pausePanel;

    void Awake()
    {
#pragma warning disable CS0618 // 형식 또는 멤버는 사용되지 않습니다.
        pausePanel = GameObject.Find("Canvas").transform.FindChild("PauseUI").gameObject;
#pragma warning restore CS0618 // 형식 또는 멤버는 사용되지 않습니다.
    }

    public void ActivePauseBt()
    {//일시정지 버튼을 눌렀을때 처리.
        //continue도 일단 같이 처리
        if (!pauseOn)
        {//일시정지 중이 아니면 일시정지.
            Time.timeScale = 0;//시간 흐름 비율 0으로
            pausePanel.SetActive(true);
            Manager.Instance._speed = 0;
        }
        else
        {//일시정지 중이면 해제.
            Time.timeScale = 1.0f;//시간 흐름 비율 1로.
            pausePanel.SetActive(false);
            Manager.Instance._speed = 0.01f;
        }

        pauseOn = !pauseOn;//불 값 반전.
    }

    public void RetryBt()
    {
        Debug.Log("게임 재시작.");
        Time.timeScale = 1.0f;//시간 초기상태로 돌려줌
        //Application.LoadLevel("Game");//1번씬 다시로드
        SceneManager.LoadScene("Main");
    }





    public void QuitBt()
    {
        Debug.Log("게임종료");
        Application.Quit();
    }
}
