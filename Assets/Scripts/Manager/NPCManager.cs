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
    public string[] npcName=new string[] {"����","�����","������","�����̱��","��Ÿȣ"};
    public int VillageChiefprogress { get; private set; } = 0;
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
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �� ��ü�� ����
        }
    }

    public void OnInteract(string npcname)
    {
        if (instance != null)
        {
            Debug.Log("NPC�� ��ȭ�� �����մϴ�.");
            GameManager.Instance.player.enabled = false;
        }
        else
        {
            Debug.Log("NPCManager�� �����ϴ�.");
        }
    
        GameManager.Instance.OnInteract(npcname);
    }
    public void PlusVillageChiefProgress()
    {
        VillageChiefprogress++;
    }
}
