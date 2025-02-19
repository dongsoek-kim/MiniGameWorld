using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;
public class NPCManager : MonoBehaviour
{
    private static NPCManager instance;
    public string[] npcName=new string[] {"촌장","토순이","성실이","겁쟁이기사","아타호"};
    public static NPCManager Instance
    {
        get {
            if (instance == null)
            {
                instance = FindObjectOfType<NPCManager>();
                if (instance == null )
                {
                    GameObject singletionObject = new GameObject("NPCManager");
                    instance = singletionObject.AddComponent<NPCManager>();
                }
            }
            
            return instance; 
        }
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
    private void Start()
    {
    }

    public void OnInteract(string npcname)
    {
        if (instance != null)
        {
            Debug.Log("NPC와 대화를 시작합니다.");
        }
        else
        {
            Debug.Log("NPCManager가 없습니다.");
        }
    
        GameManager.Instance.OnInteract(npcname);
    }
}
