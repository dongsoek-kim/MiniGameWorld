using FlappyPlane;
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
    public GameObject rock;
    public int Coin { get; set; } = 1500;
    public Action<int> DialogFinished;
    public Player player;
    public NPCHandler npcHandler;
    public Customizer costomizer;
    bool processrockBoom = false;
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

        if(rock ==null)
        {
            rock = GameObject.Find("Rock");
        }
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();  // UIManager를 씬에서 찾음
        }
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if(npcHandler=null)
        {
            npcHandler = FindObjectOfType<NPCHandler>();
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
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 이 객체를 유지
        }
    }

    private void Start()
    {

    }
    public void OnInteract(string npcName)
    {
        GameManager.Instance.npcHandler.enabled = false;
        switch (npcName)
        {
            case "VillageChief":
                Debug.Log("마을장과 대화를 시작합니다.");
                DialogVillageChiefOut();
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
            case "Shop":
                Debug.Log("상점과 대화를 시작합니다.");
                DialogShop();
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
    public void DialogShop()
    {
        uiManager.Setdialog();
        dialogManager.DialogShopOut();
    }
    private void DialogFinishedHandler(int NPCNumber)
    {
        if (NPCNumber > 0 && NPCNumber < 5)
        {
            player.StorePlayerTransform();
            Debug.Log($"저장된위치{storeTransform.position}");
            Debug.Log($"게임 {NPCNumber} 미니게임 시작!");
            MiniGameStart(npcManager.npcName[NPCNumber]);
        }
        else if (NPCNumber==5)
        {
            uiManager.SetShop();
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
                    Debug.Log("코인이 부족합니다.");
                }
            }
            else if (progress == 3)
            {
                //게임 전부 Hard모드로 클리어
            }
            else if (progress == 4)
            {
                //게임 전부 Challenge모드로 클리어   
            }
            uiManager.SetHome();
        }
    }
    public void RockBoom()
    {
        rockBoom = FindObjectOfType<RockBoom>();
        rockBoom.BoomStart();
        processrockBoom = true;
    }
    public void MiniGameStart(string name)
    {
        uiManager.SetMiniGame(name);
    }

    public void ExitUI()
    {
        uiManager.SetHome();
    }
    public void UpdateShop()
    {
        uiManager.UpdateShop();
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
        SceneManager.sceneLoaded -= OnSceneLoaded;  // 씬 로드 후 이벤트 제거
    }

    public void Costomaizing(int itemNum)
    {
        if (itemNum < 4)//머리
        {
            player.playerRender.ImageElement[8].ItemPath = "Addons/Legacy/0_Unit/0_Sprite/0_Hair/Hair_2";
        }
        else if (itemNum >= 4 && itemNum < 8)//옷
        {

        }
        else//머리색
        { 
        }
    }

    // 씬이 로드된 후 UIManager를 다시 할당
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드된 후 UIManager를 찾거나 할당
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if(npcHandler   ==null)
        {
            npcHandler = FindObjectOfType<NPCHandler>();
        }
        if (rock == null)
        {
            rock = GameObject.Find("Rock");
        }
        if (processrockBoom)
        {
            rock.SetActive(false);
        }
        player.SetPlayerTransform();
    }
}
