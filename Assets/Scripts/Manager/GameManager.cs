using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private UIManager uiManager;
    private DialogManager dialogManager;
    private MiniGameManager miniGameManager;
    private NPCManager npcManager;

    public Action<int> DialogFinished;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject singletionObject = new GameObject("GameManager");
                    instance = singletionObject.AddComponent<GameManager>();
                }
            }

            return instance;
        }

    }
    void Awake()
    {
        
        uiManager = FindObjectOfType<UIManager>();
        dialogManager = FindObjectOfType<DialogManager>();
        miniGameManager = FindObjectOfType<MiniGameManager>();
        npcManager = FindObjectOfType<NPCManager>();
        DialogFinished += DialogFinishedHandler;
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

    private void Start()
    {
    }
    public void OnInteract(string npcName)
    {
        switch(npcName)
        {
            case " Village Chief":
                Debug.Log("������� ��ȭ�� �����մϴ�.");
                break;
            case "MiniGame1 NPC":
                Debug.Log("�̴ϰ���1 NPC�� ��ȭ�� �����մϴ�.");
                DialogOut(1);
                //MiniGameStart(npcName);
                break;
            case "MiniGame2 NPC":
                Debug.Log("�̴ϰ���2 NPC�� ��ȭ�� �����մϴ�.");
                DialogOut(2);
                //MiniGameStart(npcName);
                break;
            case "MiniGame3 NPC":
                Debug.Log("�̴ϰ���3 NPC�� ��ȭ�� �����մϴ�.");
                DialogOut(3);
                //MiniGameStart(npcName);
                break;
            case "MiniGame4 NPC":
                Debug.Log("�̴ϰ���4 NPC�� ��ȭ�� �����մϴ�.");
                DialogOut(4);
                //MiniGameStart(npcName);
                break;
        }
    }
    public void DialogOut(int miniGameNumber )
    {
        uiManager.Setdialog();
        dialogManager.DialogOut(miniGameNumber);        
    }
    private void DialogFinishedHandler(int gameNumber)
    {
        Debug.Log($"���� {gameNumber} �̴ϰ��� ����!");
        MiniGameStart(npcManager.npcName[gameNumber]);
        
    }

    public void MiniGameStart(string name)
    {
        uiManager.SetMiniGame(name);
    }
    
    public void ExitMiniGame()
    {
        uiManager.SetHome();
    }

    public void OpenShop()
    {
        uiManager.SetShop();
    }
}   
