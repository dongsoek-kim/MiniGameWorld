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
        public int difficulty;
        public Action GameClear;
        // 게임 난이도 설정
        public void SetGameDifficulty(int newDifficulty)
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
        private void Update()
        {
            IsClear();
        }
        public void GameOver()
        {
            uiManager.SetGameOver();
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ONGameClear()
        {
            uiManager.SetGameClear();
        }
        public void AddScore(int score)
        {
            currentScore += score;
            uiManager.UpdateScore(currentScore);
        }
        public void IsClear()
        {
            switch (difficulty)
            {
                case 0://easy
                    if (currentScore >= 7)
                    {
                        Debug.Log("클리어");
                        GameClear?.Invoke();
                        MiniGameManager.Instance.StageClear?.Invoke(1, 1);//결합도 문제
                    }
                    break;
                case 1://normal
                    if (currentScore >= 15)
                    {
                        Debug.Log("클리어");
                        GameClear?.Invoke();
                        MiniGameManager.Instance.StageClear?.Invoke(2, 1);//결합도 문제
                    }
                    break;
                case 2://hard

                    if (currentScore >= 20)
                    {
                        Debug.Log("클리어");
                        GameClear?.Invoke();
                        MiniGameManager.Instance.StageClear?.Invoke(3, 1);//결합도 문제
                    }
                    break;
                case 3://challenge

                    if (currentScore >= 30)
                    {
                        Debug.Log("클리어");
                        GameClear?.Invoke();
                        //MiniGameManager.Instance.StageClear?.Invoke(4, 1);//결합도 문제
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
