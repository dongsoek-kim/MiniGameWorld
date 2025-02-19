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
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 이 객체를 유지
        }
    }
    public void OnInteract(string npcName)
    {
        switch(npcName)
        {
            case " Village Chief":
                Debug.Log("마을장과 대화를 시작합니다.");
                break;
            case "MiniGame1 NPC":
                Debug.Log("미니게임1 NPC와 대화를 시작합니다.");
                DialogOut(1);
                break;
            case "MiniGame2 NPC":
                Debug.Log("미니게임2 NPC와 대화를 시작합니다.");
                DialogOut(2);
                break;
            case "MiniGame3 NPC":
                Debug.Log("미니게임3 NPC와 대화를 시작합니다.");
                DialogOut(3);
                break;
            case "MiniGame4 NPC":
                Debug.Log("미니게임4 NPC와 대화를 시작합니다.");
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
