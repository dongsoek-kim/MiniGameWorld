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

    // 5���� �̴ϰ��� ���� ���� (0~4: ���൵, 0�� ��Ŭ����, 1~4�� �������� Ŭ����)
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
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �� ��ü�� ����
        }
    }


    // ����: Ư�� �̴ϰ����� ���൵�� ����
    public void StageClearHandler(int gameNumber, int progress)
    {
    
        if(progress==3&& stage3Clear[gameNumber])//�̹� 3�������� Ŭ����
        {
            if (ischallengeMode)//ç���� ��尡 ���ȴٸ�
                minigameProgress[gameNumber] = progress;
            else
                Debug.Log("ç���� ��尡 �ƴմϴ�.");
            GameManager.Instance.Coin += 5;
        }
        else if(progress==3&& !stage3Clear[gameNumber])
        {
            GameManager.Instance.Coin += 500;
            stage3Clear[gameNumber] = true;
        }
        else if(progress>4)
        {
            Debug.Log("���൵�� 4�� ���� �� �����ϴ�.");
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
        // ���� �ر� ���� üũ
        if (CheckGameUnlock(gameNumber, difficulty))
        {
            // �ش� ������ �̵� (���� �� �̸��� "Game" + ���� ��ȣ)
            Debug.Log($"���ӹ�ȣ{ gameNumber}");
            Debug.Log($"���̵�{difficulty}");
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
            Debug.Log("������ ��� �����Դϴ�.");
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


