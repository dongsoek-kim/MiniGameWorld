using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private UIManager uiManager;
    private DialogManager dialogManager;
    private MiniGameManager miniGameManager;   
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
                break;
            case "MiniGame2 NPC":
                Debug.Log("�̴ϰ���2 NPC�� ��ȭ�� �����մϴ�.");
                DialogOut(2);
                break;
            case "MiniGame3 NPC":
                Debug.Log("�̴ϰ���3 NPC�� ��ȭ�� �����մϴ�.");
                DialogOut(3);
                break;
            case "MiniGame4 NPC":
                Debug.Log("�̴ϰ���4 NPC�� ��ȭ�� �����մϴ�.");
                DialogOut(4);
                break;
        }
    }
    public void DialogOut(int miniGameNumber )
    {
        uiManager.Setdialog();
        dialogManager.DialogOut(miniGameNumber);        
    }
    public void MiniGameStart()
    {
        uiManager.SetMiniGame();
    }

    public void OpenShop()
    {
        uiManager.SetShop();
    }
}   
