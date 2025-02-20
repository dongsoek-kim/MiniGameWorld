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
                buttonText.text = "������";
            }
            else
            {
                buttonText.text = "������";
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
                buttonText.text = "������";
            }
            else
            {
                buttonText.text = "������";
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

    // ��ư Ŭ�� �� GameNumber�� ���̵� ����
    private void OnButtonClicked(int itemNum)
    {
        // �̴ϰ��� �Ŵ����� ���� ��ȣ�� ���̵� ����
        Debug.Log($"{itemNum}������");
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
