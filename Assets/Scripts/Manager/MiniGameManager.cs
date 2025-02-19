using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
//using FlappyPlane;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager instance;
    private bool ischallengeMode = false;
    private bool[] stage3Clear = new bool[4];
    public static MiniGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MiniGameManager>();
                if (instance == null)
                {
                    GameObject singletionObject = new GameObject("MinigameManager");
                    instance = singletionObject.AddComponent<MiniGameManager>();
                }
            }

            return instance;
        }
    }
    [SerializeField]private UIManager uiManager;

    // 5개의 미니게임 진행 상태 (0~4: 진행도, 0은 미클리어, 1~4는 스테이지 클리어)
    public int[] minigameProgress = new int[4];
    public Action<int, int> StageClear;

    void Awake()
    {
        StageClear += StageClearHandler;
        uiManager = FindObjectOfType<UIManager>();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 이 객체를 유지
        }
    }


    // 예시: 특정 미니게임의 진행도를 설정
    public void StageClearHandler(int gameNumber, int progress)
    {
    
        if(progress==3&& stage3Clear[gameNumber])//이미 3스테이지 클리어
        {
            if (ischallengeMode)//챌린지 모드가 열렸다면
                minigameProgress[gameNumber] = progress;
            else
                Debug.Log("챌린지 모드가 아닙니다.");
            GameManager.Instance.Coin += 5;
        }
        else if(progress==3&& !stage3Clear[gameNumber])
        {
            GameManager.Instance.Coin += 500;
            stage3Clear[gameNumber] = true;
        }
        else if(progress>4)
        {
            Debug.Log("진행도가 4를 넘을 수 없습니다.");
            GameManager.Instance.Coin += 5;
        }
        else if (gameNumber >= 0 && gameNumber < minigameProgress.Length && minigameProgress[gameNumber] < progress)
        {
            minigameProgress[gameNumber] = progress;
            GameManager.Instance.Coin += 500;
        }
        else
            GameManager.Instance.Coin += 5;
    }
    public void ReceiveGameData(int gameNumber, int difficulty)
    {
        // 게임 해금 여부 체크
        if (CheckGameUnlock(gameNumber, difficulty))
        {
            // 해당 씬으로 이동 (게임 씬 이름은 "Game" + 게임 번호)
            Debug.Log($"게임번호{ gameNumber}");
            Debug.Log($"난이도{difficulty}");
            switch(gameNumber)
            {
                case 0:
                    Stack.GameManager.SetGameDifficulty(difficulty);
                    SceneManager.LoadScene($"MiniGame{gameNumber + 1}");
                    break;
                case 1:
                    FlappyPlane.GameManager.SetGameDifficulty(difficulty);
                    SceneManager.LoadScene($"MiniGame{gameNumber + 1}");
                    break;
                case 2:
                    //SceneManager.LoadScene($"MiniGame{gameNumber + 1}");
                    break;
                case 3:
                    //SceneManager.LoadScene($"MiniGame{gameNumber + 1}");
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log("게임이 잠금 상태입니다.");
        }
    }
    private bool CheckGameUnlock(int gameNumber, int difficulty)
    {
        if (minigameProgress[gameNumber]<difficulty)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}


