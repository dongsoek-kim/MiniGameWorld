using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using FlappyPlane;

public class MiniGameManager : MonoBehaviour
{
    private static MiniGameManager instance;
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
    private UIManager uiManager;

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
        if (gameNumber >= 0 && gameNumber < minigameProgress.Length)
        {
            minigameProgress[gameNumber] = progress;
        }
    }
    public void ReceiveGameData(int gameNumber, int difficulty)
    {
        // 게임 해금 여부 체크
        if (CheckGameUnlock(gameNumber, difficulty))
        {
            // 해당 씬으로 이동 (게임 씬 이름은 "Game" + 게임 번호)
            Debug.Log($"게임번호{ gameNumber}");
            Debug.Log($"난이도{difficulty}");
            FlappyPlane.GameManager.SetGameDifficulty(difficulty);
            SceneManager.LoadScene($"MiniGame{gameNumber+1}");
            // 난이도 전달 (씬 간 난이도 전달)
            //SceneManager.sceneLoaded += (scene, mode) => PassDifficultyToScene(difficulty);
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
    private void PassDifficultyToScene(int diffculty)
    {   //게임 씬에 있는 GameManager에 난이도 전달
        //namespace.GameManager.Instance.SetDifficulty(diffculty);를 이용해 전달
    }
}


