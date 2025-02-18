using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private static DialogManager instance;
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

    public TextMeshProUGUI dialogText;
    public GameObject dialogUI;

    [System.Serializable]
    public class DialogData
    {
        public string dialogText;
    }


    void Awake()
    {
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
    
    //미니게임매니저에 진행도에 따라 대사 수정
    public void Dialog(int miniGameNumber)
    {
        if (dialogUI == null || dialogText == null)
        {
            Debug.LogError("DialogUI 또는 DialogText가 할당되지 않았습니다.");
            return;
        }

        // 미니게임 진행도 가져오기
        int gameProgress = MinigameManager.instance.minigameProgress[miniGameNumber - 1];

        // JSON에서 다이얼로그 데이터를 가져옵니다.
        string fileName = "Dialog_" + miniGameNumber + ".json";
        string folderPath = Path.Combine(Application.streamingAssetsPath, "Dialogs"); // Dialog 폴더
        string filePath = Path.Combine(folderPath, fileName); // 폴더와 파일명 결합

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            DialogData[] dialogDataArray = JsonHelper.FromJson<DialogData>(jsonData);

            // 게임 진행도에 따라 다이얼로그 설정
            if (gameProgress < dialogDataArray.Length)
            {
                dialogText.text = dialogDataArray[gameProgress].dialogText;
            }
            else
            {
                dialogText.text = "이 미니게임은 아직 진행되지 않았습니다.";
            }

            dialogUI.SetActive(true);
            StartCoroutine(WaitForKeyPress());
        }
        else
        {
            Debug.LogError("Dialog JSON 파일이 존재하지 않습니다: " + filePath);
        }
    }

    private IEnumerator WaitForKeyPress()
    {
        // 아무 키나 입력할 때까지 대기
        yield return new WaitUntil(() => Input.anyKeyDown);

        // 키가 입력되면 다이얼로그 UI 비활성화
        Debug.Log("키입력");
        //dialogUI.SetActive(false);
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
}
