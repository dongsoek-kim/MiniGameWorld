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
        if (gameNumber >= 0 && gameNumber < minigameProgress.Length)
        {
            minigameProgress[gameNumber] = progress;
        }
    }
    public void ReceiveGameData(int gameNumber, int difficulty)
    {
        // ���� �ر� ���� üũ
        if (CheckGameUnlock(gameNumber, difficulty))
        {
            // �ش� ������ �̵� (���� �� �̸��� "Game" + ���� ��ȣ)
            Debug.Log($"���ӹ�ȣ{ gameNumber}");
            Debug.Log($"���̵�{difficulty}");
            FlappyPlane.GameManager.SetGameDifficulty(difficulty);
            SceneManager.LoadScene($"MiniGame{gameNumber+1}");
            // ���̵� ���� (�� �� ���̵� ����)
            //SceneManager.sceneLoaded += (scene, mode) => PassDifficultyToScene(difficulty);
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
    private void PassDifficultyToScene(int diffculty)
    {   //���� ���� �ִ� GameManager�� ���̵� ����
        //namespace.GameManager.Instance.SetDifficulty(diffculty);�� �̿��� ����
    }
}


