using System;
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

    public void SelectMiniGame(string name)
    {
        for (int i = 0; i < miniGameList.Length; i++)
        {
            miniGameList[i].SetActive(false);
        }

        int npcIndex = Array.IndexOf(NPCManager.Instance.npcName, name);

        if (npcIndex >= 1 && npcIndex < miniGameList.Length)
        {
            miniGameList[npcIndex - 1].SetActive(true);
        }
    }

}