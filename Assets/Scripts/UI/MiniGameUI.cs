using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MiniGameUI : BaseUI
{
    [SerializeField] private GameObject[] miniGameList;
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

    private void SelectMiniGame(string name)
    {
        miniGameList[0].SetActive(false);
        miniGameList[1].SetActive(false);
        miniGameList[2].SetActive(false);
        miniGameList[3].SetActive(false);
        switch (name)
        {
            case "MiniGame1 NPC":
                miniGameList[0].SetActive(true);
                break;
            case "MiniGame2 NPC":
                miniGameList[1].SetActive(true);
                break;
            case "MiniGame3 NPC":
                miniGameList[2].SetActive(true);
                break;
            case "MiniGame4 NPC":
                miniGameList[3].SetActive(true);
                break;
            default:
                break;
        }
    }
}
