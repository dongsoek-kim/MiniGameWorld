using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Home,
    Customazing,
    Dialog,
    MiniGame,
    Shop,
    Leaderboard
}

public class UIManager : MonoBehaviour
{

    HomeUI homeUI;
    CustomazingUI customazingUI;
    DialogUI dialogUI;
    MiniGameUI miniGameUI;
    ShopUI shopUI;
    LeaderBoardUI LeaderBoardUI;
    private UIState currentState;


    private void Awake()
    {

        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init(this);
        customazingUI = GetComponentInChildren<CustomazingUI>(true);
        customazingUI.Init(this);
        dialogUI = GetComponentInChildren<DialogUI>(true);
        dialogUI.Init(this);
        miniGameUI = GetComponentInChildren<MiniGameUI>(true);
        miniGameUI.Init(this);
        shopUI = GetComponentInChildren<ShopUI>(true);
        shopUI.Init(this);
        LeaderBoardUI = GetComponentInChildren<LeaderBoardUI>(true);
        LeaderBoardUI.Init(this);
        ChangeState(UIState.Home);
    }

  
    public void SetHome()
    {
        ChangeState(UIState.Home);
        GameManager.Instance.player.enabled = true;
    }
    public void SetCostomazing()
    {
        ChangeState(UIState.Customazing);
    }

    public void Setdialog()
    {
        ChangeState(UIState.Dialog);
    }

    public void SetMiniGame(string name)
    {
        ChangeState(UIState.MiniGame);
        miniGameUI.SelectMiniGame(name);
    }

    public void SetShop()
    {
        ChangeState(UIState.Shop);
    }

    private void SetLeaderBoard()
    {
        ChangeState(UIState.Leaderboard);
    }
    //public void ChangeWave(int waveIndex)
    //{
    //    minigameUI.UpdateWaveText(waveIndex);
    //}
    public void SetDailog(string name,string[] dialog)
    {
        dialogUI.SetText(name, dialog);
    }
    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI.SetActive(currentState);
        customazingUI.SetActive(currentState);
        dialogUI.SetActive(currentState);
        miniGameUI.SetActive(currentState);
        shopUI.SetActive(currentState);
    }
}
