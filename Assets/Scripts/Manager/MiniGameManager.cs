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
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �� ��ü�� ����
        }
    }

    // 5���� �̴ϰ��� ���� ���� (0~4: ���൵, 0�� ��Ŭ����, 1~4�� �������� Ŭ����)
    public int[] minigameProgress = new int[5];
    public event Action<int, int> StageClear;

    // ����: Ư�� �̴ϰ����� ���൵�� ����
    public void SetMinigameProgress(int gameNumber, int progress)
    {
        if (gameNumber >= 0 && gameNumber < minigameProgress.Length)
        {
            minigameProgress[gameNumber] = progress;
        }
        StageClear?.Invoke(gameNumber,progress);//�������� Ŭ����� ȣ���� �̺�Ʈ
    }

    private void Start()
    {
        StageClear += SetMinigameProgress;
    }

}


