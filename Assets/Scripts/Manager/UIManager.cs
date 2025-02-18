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
    Shop
}

public class UIManager : MonoBehaviour
{

    HomeUI homeUI;
    CustomazingUI customazingUI;
    DialogUI dialogUI;
    MiniGameUI miniGameUI;
    ShopUI shopUI;
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

        ChangeState(UIState.Home);
    }



    public void SetCostomazing()
    {
        ChangeState(UIState.Customazing);
    }

    public void Setdialog()
    {
        ChangeState(UIState.Dialog);
    }

    public void SetMiniGame()
    {
        ChangeState(UIState.MiniGame);
    }

    public void SetShop()
    {
        ChangeState(UIState.Shop);
    }

    //public void ChangeWave(int waveIndex)
    //{
    //    minigameUI.UpdateWaveText(waveIndex);
    //}
    public void SetDailog(string name,string[] dialog)
    {
        Debug.Log("SetDailog");
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
