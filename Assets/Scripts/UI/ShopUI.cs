using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShopUI : BaseUI
{
    public Button[] button;
    [SerializeField]private TextMeshProUGUI buttonText;
    [SerializeField]TextMeshProUGUI coinText;
    public Button Exitbutton;
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        Exitbutton.onClick.AddListener(OnClickExitButton);
        Coin();
        for (int i = 0;i< button.Length; i++)
        {
            buttonText = button[i].GetComponentInChildren<TextMeshProUGUI>();
            if (ShopManager.Instance.itemHas[i] == false)
            {
                buttonText.text = "5G";
            }
            else if (ShopManager.Instance.hairNum==i || ShopManager.Instance.bodyNum ==i|| ShopManager.Instance.colorNum==i)
            {
                buttonText.text = "착용중";
            }
            else
            {
                buttonText.text = "보유중";
            }
        }
        SetButtons();
    }
    public void UpdateShop()
    {
        base.Init(uiManager);
        Coin();
        for (int i = 0; i < button.Length; i++)
        {
            buttonText = button[i].GetComponentInChildren<TextMeshProUGUI>();
            if (ShopManager.Instance.itemHas[i] == false)
            {
                buttonText.text = "5G";
            }
            else if (ShopManager.Instance.hairNum == i || ShopManager.Instance.bodyNum == i || ShopManager.Instance.colorNum == i)
            {
                buttonText.text = "착용중";
            }
            else
            {
                buttonText.text = "보유중";
            }
        }
    }   
    public void OnClickExitButton()
    {
        GameManager.Instance.ExitUI();
    }
    private void SetButtons()
    {
        for (int i = 0; i < button.Length; i++)
        {
            int index = i;
            button[i].onClick.RemoveAllListeners();
            button[i].onClick.AddListener(() => OnButtonClicked(index));
        }
    }

    // 버튼 클릭 시 GameNumber와 난이도 전달
    private void OnButtonClicked(int itemNum)
    {
        // 미니게임 매니저에 게임 번호와 난이도 전달
        Debug.Log($"{itemNum}눌렀다");
        ShopManager.Instance.ReceiveShopData(itemNum);
    }
    public void Coin()
    {
        coinText.text = GameManager.Instance.Coin.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Shop;
    }
}
