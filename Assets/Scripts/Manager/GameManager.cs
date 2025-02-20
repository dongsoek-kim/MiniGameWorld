using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField]
    private UIManager uiManager;
    private DialogManager dialogManager;
    private MiniGameManager miniGameManager;
    private NPCManager npcManager;
    private RockBoom rockBoom;
    public Transform storeTransform;
    public int Coin { get; set; } = 1500;
    public Action<int> DialogFinished;
    public MainPlayerController player;
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
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();  // UIManager�� ������ ã��
        }
        if (player == null)
        {
            player = FindObjectOfType<MainPlayerController>();
        }
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
        switch (npcName)
        {
            case "VillageChief":
                Debug.Log("������� ��ȭ�� �����մϴ�.");
                DialogVillageChiefOut();
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

    public void DialogVillageChiefOut()
    {
        uiManager.Setdialog();
        dialogManager.DialogVillageChiefOut();
    }
    public void DialogOut(int miniGameNumber)
    {
        uiManager.Setdialog();
        dialogManager.DialogOut(miniGameNumber);
    }
    private void DialogFinishedHandler(int NPCNumber)
    {
        if (NPCNumber > 0 && NPCNumber < 5)
        {
            player.StorePlayerTransform();
            Debug.Log($"�������ġ{storeTransform.position}");
            Debug.Log($"���� {NPCNumber} �̴ϰ��� ����!");
            MiniGameStart(npcManager.npcName[NPCNumber]);
        }
        else if (NPCNumber == 0)
        {
            int progress = NPCManager.Instance.VillageChiefprogress;
            if (progress == 0 || progress == 2)
            {
                NPCManager.Instance.PlusVillageChiefProgress();
            }
            else if (progress == 1)
            {
                if (Coin >= 1500)
                {
                    Coin -= 1500;
                    NPCManager.Instance.PlusVillageChiefProgress();
                    RockBoom();
                }
                else
                {
                    Debug.Log("������ �����մϴ�.");
                }
            }
            else if (progress == 3)
            {
                //���� ���� Hard���� Ŭ����
            }
            else if (progress == 4)
            {
                //���� ���� Challenge���� Ŭ����   
            }
            uiManager.SetHome();
        }
    }
    public void RockBoom()
    {
        rockBoom = FindObjectOfType<RockBoom>();
        rockBoom.BoomStart();
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
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // �� �ε� �� �̺�Ʈ ����
    }

    // ���� �ε�� �� UIManager�� �ٽ� �Ҵ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�� �� UIManager�� ã�ų� �Ҵ�
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        if (player == null)
        {
            player = FindObjectOfType<MainPlayerController>();
        }
        player.SetPlayerTransform();
    }
}
