using FlappyPlane;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private static ShopManager instance;
    Player player = GameManager.Instance.player;
    public static ShopManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ShopManager>();
                if (instance == null)
                {
                    GameObject singletionObject = new GameObject("ShopManager");
                    instance = singletionObject.AddComponent<ShopManager>();
                }
            }

            return instance;
        }
    }
    public bool[] itemHas = new bool[12];
    public int hairNum { get; set; }
    public int bodyNum { get; set; }
    public int colorNum { get; set; }
    void Awake()
    {
        itemHas[0] = true;
        itemHas[4] = true;
        itemHas[8] = true;
        hairNum = 0;
        bodyNum = 4;
        colorNum = 8;
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


    public void ReceiveShopData(int itemNum)
    {
        if(itemHas[itemNum])
        {
            GameManager.Instance.Costomaizing(itemNum);
        }
        else
        {
            if(GameManager.Instance.Coin<5)
            {
                Debug.Log("돈이없습니다.");
            }
            else 
            { 
                Debug.Log("구매완료");
                GameManager.Instance.Coin -= 5; 
                itemHas[itemNum] = true;
            }
        }
        
        GameManager.Instance.UpdateShop();
    }
}
