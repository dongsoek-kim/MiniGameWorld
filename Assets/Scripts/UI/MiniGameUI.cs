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

    // Ȱ��ȭ�� UI �ε����� ���� (0~3) 
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
        // �� ��ư�� Ŭ�� �����ʸ� �߰�
        easyButton.onClick.AddListener(() => OnButtonClicked(gameNumber, 0));
        normalButton.onClick.AddListener(() => OnButtonClicked(gameNumber, 1));
        hardButton.onClick.AddListener(() => OnButtonClicked(gameNumber, 2));
        challengeButton.onClick.AddListener(() => OnButtonClicked(gameNumber, 3));
    }

    // ��ư Ŭ�� �� GameNumber�� ���̵� ����
    private void OnButtonClicked(int gameNumber, int difficulty)
    {
        // �̴ϰ��� �Ŵ����� ���� ��ȣ�� ���̵� ����
        Debug.Log($"OnButtonClicked invoked with gameNumber: {gameNumber}, difficulty: {difficulty}");
        MiniGameManager.Instance.ReceiveGameData(gameNumber, difficulty);
    }

}