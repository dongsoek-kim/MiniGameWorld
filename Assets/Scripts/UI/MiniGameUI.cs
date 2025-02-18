using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGameUI : BaseUI
{
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }
    private void Start()
    {
    }

    protected override UIState GetUIState()
    {
        return UIState.MiniGame;
    }
}
