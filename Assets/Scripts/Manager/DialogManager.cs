using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    private static DialogManager instance;
    [SerializeField] private UIManager uiManager;
    public static DialogManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogManager>();
                if (instance == null)
                {
                    GameObject singletionObject = new GameObject("DialogManager");
                    instance = singletionObject.AddComponent<DialogManager>();
                }
            }

            return instance;
        }
    }

    //public TextMeshProUGUI dialogText;
    //public GameObject dialogUI;
    [System.Serializable]
    public class DialogData
    {
        public string[] dialogText;
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

    public void DialogOut(int miniGameNumber)
    {
        LoadDialog(miniGameNumber);
    }
    //미니게임매니저에 진행도에 따라 대사 수정
    private void LoadDialog(int miniGameNumber)
    {

        // 미니게임 진행도 가져오기
        int gameProgress = MiniGameManager.Instance.minigameProgress[miniGameNumber - 1];

        string fileName = "Dialog_" + miniGameNumber + ".json";
        string folderPath = Path.Combine(Application.streamingAssetsPath, "Dialogs");
        string filePath = Path.Combine(folderPath, fileName); 

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            DialogData[] dialogDataArray = JsonHelper.FromJson<DialogData>(jsonData);

            if (gameProgress < dialogDataArray.Length)
            {
                uiManager.SetDailog(NPCManager.Instance.npcName[miniGameNumber], dialogDataArray[gameProgress].dialogText);
            }
            else
            {
                uiManager.SetDailog("d",new string[] { "이 미니게임은 아직 진행되지 않았습니다." });
            }
        }
        else
        {
            Debug.LogError("Dialog JSON 파일이 존재하지 않습니다: " + filePath);
        }
    }


    // JSON 데이터를 다루는 유틸리티
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            // 배열로 파싱하는 방법
            string newJson = "{\"items\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.items;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] items;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // 씬 로드 후 이벤트 제거
    }

    // 씬이 로드된 후 UIManager를 다시 할당
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드된 후 UIManager를 찾거나 할당
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
    }
}
