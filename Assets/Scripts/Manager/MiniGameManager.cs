using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance;

    // 5개의 미니게임 진행 상태 (0~4: 진행도, 0은 미클리어, 1~4는 스테이지 클리어)
    public int[] minigameProgress = new int[5];
    public event Action<int, int> StageClear;
    void Awake()
    {
        // 싱글톤 패턴 구현
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 예시: 특정 미니게임의 진행도를 설정
    public void SetMinigameProgress(int gameNumber, int progress)
    {
        if (gameNumber >= 0 && gameNumber < minigameProgress.Length)
        {
            minigameProgress[gameNumber] = progress;
        }
        StageClear?.Invoke(gameNumber,progress);
    }

    private void Start()
    {
        StageClear += SetMinigameProgress;
        minigameProgress[0] = 4;//테스트코드
    }
}


