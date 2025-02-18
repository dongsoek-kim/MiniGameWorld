using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private UIManager uiManager;
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
    public void DialogOut(int miniGameNumber )
    {
        uiManager.Setdialog();
        DialogManager.Instance.DialogOut(miniGameNumber);        
    }
    public void MiniGameStart()
    {
        uiManager.SetMiniGame();
    }
}   
