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
        Exitbutton.onClick.RemoveAllListeners();
        Exitbutton.onClick.AddListener(OnClickExitButton);
    }
    public Button Exitbutton;
    public Button[] easyButtons;
    public Button[] normalButtons;
    public Button[] hardButtons;
    public Button[] challengeButtons;

    // 활성화된 UI 인덱스를 추적 (0~3) 
    //public GameObject[] miniGameUIs;


    protected override UIState GetUIState()
    {
        return UIState.MiniGame;
    }
    public void OnClickExitButton()
    {
        GameManager.Instance.ExitMiniGame();
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
            SetButtons(npcIndex - 1);
        }
    }
    private void SetButtons(int gameNumber)
    {
        Button easyButton = easyButtons[gameNumber];
        Button normalButton = normalButtons[gameNumber];
        Button hardButton = hardButtons[gameNumber];
        Button challengeButton = challengeButtons[gameNumber];

        easyButton.onClick.RemoveAllListeners();
        normalButton.onClick.RemoveAllListeners();
        hardButton.onClick.RemoveAllListeners();
        challengeButton.onClick.RemoveAllListeners();
        // 각 버튼에 클릭 리스너를 추가
        easyButton.onClick.AddListener(() => OnButtonClicked(gameNumber, 0));
        normalButton.onClick.AddListener(() => OnButtonClicked(gameNumber, 1));
        hardButton.onClick.AddListener(() => OnButtonClicked(gameNumber, 2));
        challengeButton.onClick.AddListener(() => OnButtonClicked(gameNumber, 3));
    }

    // 버튼 클릭 시 GameNumber와 난이도 전달
    private void OnButtonClicked(int gameNumber, int difficulty)
    {
        // 미니게임 매니저에 게임 번호와 난이도 전달
        Debug.Log($"OnButtonClicked invoked with gameNumber: {gameNumber}, difficulty: {difficulty}");
        MiniGameManager.Instance.ReceiveGameData(gameNumber, difficulty);
    }

}