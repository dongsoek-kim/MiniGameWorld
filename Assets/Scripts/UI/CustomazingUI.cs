using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomazingUI : BaseUI
{
    // Start is called before the first frame update
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }
    protected override UIState GetUIState()
    {
        return UIState.Customazing;
    }
}
