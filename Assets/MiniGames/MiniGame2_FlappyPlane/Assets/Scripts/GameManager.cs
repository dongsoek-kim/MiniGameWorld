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
        private static int difficulty;

        // 게임 난이도 설정
        public static void SetGameDifficulty(int newDifficulty)
        {
            difficulty = newDifficulty;
        }
        private int currentScore = 0;
        UIManager uiManager;
        public UIManager UIManager { get { return uiManager; } }
        public void Awake()
        {
            gameManager = this;
            uiManager = FindObjectOfType<UIManager>();
        }
        private void Start()
        {
            uiManager.UpdateScore(0);
        }
        public void GameOver()
        {
            IsClear();
            uiManager.SetRestart();
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void AddScore(int score)
        {
            currentScore += score;
            uiManager.UpdateScore(currentScore);
        }
        public void IsClear()
        {
            switch(difficulty)
            {
                case 0:
                    if (currentScore >= 3)
                    {
                        Debug.Log("클리어");
                        MiniGameManager.Instance.StageClear?.Invoke(1, 1);//결합도 문제
                    }
                    break;
            }
        }
    }
}
