using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

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
    void Awake()
    {

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

    // 5개의 미니게임 진행 상태 (0~4: 진행도, 0은 미클리어, 1~4는 스테이지 클리어)
    public int[] minigameProgress = new int[5];
    public event Action<int, int> StageClear;

    // 예시: 특정 미니게임의 진행도를 설정
    public void SetMinigameProgress(int gameNumber, int progress)
    {
        if (gameNumber >= 0 && gameNumber < minigameProgress.Length)
        {
            minigameProgress[gameNumber] = progress;
        }
        StageClear?.Invoke(gameNumber,progress);//스테이지 클리어시 호출할 이벤트
    }

    private void Start()
    {
        StageClear += SetMinigameProgress;
    }

}


