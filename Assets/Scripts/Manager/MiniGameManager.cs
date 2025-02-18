using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager instance;

    // 5���� �̴ϰ��� ���� ���� (0~4: ���൵, 0�� ��Ŭ����, 1~4�� �������� Ŭ����)
    public int[] minigameProgress = new int[5];
    public event Action<int, int> StageClear;
    void Awake()
    {
        // �̱��� ���� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ����: Ư�� �̴ϰ����� ���൵�� ����
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
        minigameProgress[0] = 4;//�׽�Ʈ�ڵ�
    }
}


