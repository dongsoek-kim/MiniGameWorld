using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
public class NPCManager : MonoBehaviour
{
    private static NPCManager instance;
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

    public void OnIntercate()
    {
        GameManager.Instance.DialogOut(1);
    }
}
