using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardUI : BaseUI
{
    // Start is called before the first frame update
    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override UIState GetUIState()
    {
        return UIState.Leaderboard;
    }
}
