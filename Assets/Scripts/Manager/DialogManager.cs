using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private static DialogManager instance;
    private UIManager uiManager;
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
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �� ��ü�� ����
        }
    }

    public void DialogOut(int miniGameNumber)
    {
        LoadDialog(miniGameNumber);
        //uiManager.SetMiniGame();

    }
    //�̴ϰ��ӸŴ����� ���൵�� ���� ��� ����
    private void LoadDialog(int miniGameNumber)
    {

        // �̴ϰ��� ���൵ ��������
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
                uiManager.SetDailog("d", dialogDataArray[gameProgress].dialogText);
            }
            else
            {
                uiManager.SetDailog("d",new string[] { "�� �̴ϰ����� ���� ������� �ʾҽ��ϴ�." });
            }
        }
        else
        {
            Debug.LogError("Dialog JSON ������ �������� �ʽ��ϴ�: " + filePath);
        }
    }


    // JSON �����͸� �ٷ�� ��ƿ��Ƽ
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            // �迭�� �Ľ��ϴ� ���
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
