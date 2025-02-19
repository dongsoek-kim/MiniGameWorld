using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyPlame
{
    public class GameManager : MonoBehaviour
    {
        static GameManager gameManager;
        public static GameManager Instance { get { return gameManager; } }

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
    }
}
