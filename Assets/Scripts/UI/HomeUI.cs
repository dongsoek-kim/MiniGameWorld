using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] TextMeshProUGUI coinText;
    public void Update()
    {
        Coin();
    }

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }

    public void Coin()
    {
        coinText.text = GameManager.Instance.Coin.ToString();
    }


    protected override UIState GetUIState()
    {
        return UIState.Home;
    }
}