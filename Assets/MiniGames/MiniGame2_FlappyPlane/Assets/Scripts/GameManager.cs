using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyPlane
{
    public class GameManager : MonoBehaviour
    {
        static GameManager gameManager;
        public static GameManager Instance { get { return gameManager; } }
        public static int difficulty;
        private bool clear;
        public Action GameClear;
        // ���� ���̵� ����
        public static void SetGameDifficulty(int newDifficulty)
        {
            difficulty = newDifficulty;
        }
        private int currentScore = 0;
        UIManager uiManager;
        public UIManager UIManager { get { return uiManager; } }
        public void Awake()
        {
            GameClear += ONGameClear;
            gameManager = this;
            uiManager = FindObjectOfType<UIManager>();
        }
        private void Start()
        {
            uiManager.UpdateScore(0);
        }
        public void GameOver()
        {
            uiManager.SetGameOver();
        }
        public void ONGameClear()
        {
            uiManager.SetGameClear();
        }
        public void AddScore(int score)
        {
            currentScore += score;
            uiManager.UpdateScore(currentScore);
            IsClear();
        }
        public void IsClear()
        {

            if (!clear)
            {
                switch (difficulty)

                {
                    case 0://easy
                        if (currentScore >= 5)
                        {
                            Debug.Log("Ŭ����");
                            clear = true;
                            GameClear?.Invoke();
                            MiniGameManager.Instance.StageClear?.Invoke(1, 1);//���յ� ����
                        }
                        break;
                    case 1://normal
                        if (currentScore >= 7)
                        {
                            Debug.Log("Ŭ����");
                            clear = true;
                            GameClear?.Invoke();
                            MiniGameManager.Instance.StageClear?.Invoke(1, 2);//���յ� ����
                        }
                        break;
                    case 2://hard

                        if (currentScore >= 9)
                        {
                            Debug.Log("Ŭ����");
                            clear = true;
                            GameClear?.Invoke();
                            MiniGameManager.Instance.StageClear?.Invoke(1, 3);//���յ� ����
                        }
                        break;
                    case 3://challenge

                        if (currentScore >= 11)
                        {
                            Debug.Log("Ŭ����");
                            clear = true;
                            GameClear?.Invoke();
                            MiniGameManager.Instance.StageClear?.Invoke(1, 4);//���յ� ����
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
