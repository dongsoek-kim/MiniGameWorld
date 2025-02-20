using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace TopDown
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public PlayerController player { get; private set; }
        private ResourceController _playerResourceController;
        public static int difficulty;
        public Action GameClear;
        [SerializeField] private int currentWaveIndex = 0;

        private EnemyManager enemyManager;
        private UIManager uiManager;
        public static bool isFirstLoading = true;
        private bool clear;
        public static void SetGameDifficulty(int newDifficulty)
        {
            difficulty = newDifficulty;
        }
        private void Awake()
        {
            GameClear += ONGameClear;
            instance = this;
            player = FindObjectOfType<PlayerController>();
            player.Init(this);

            uiManager = FindObjectOfType<UIManager>();

            _playerResourceController = player.GetComponent<ResourceController>();
            _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
            _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

            enemyManager = GetComponentInChildren<EnemyManager>();
            enemyManager.Init(this);
        }

        private void Start()
        {
          StartGame();        
        }

        public void StartGame()
        {
            StartNextWave();
        }

        void StartNextWave()
        {
            currentWaveIndex += 1;
            uiManager.ChangeWave(currentWaveIndex);
            enemyManager.StartWave(1 + currentWaveIndex / 5);
        }

        public void EndOfWave()
        {
            IsClear();
            if (!clear)
            {
                StartNextWave();
            }
            else
            {
                SceneManager.LoadScene("MainScene");
            }
        }
        public void ONGameClear()
        {
            uiManager.SetGameClear();
        }
        public void GameOver()
        {
            enemyManager.StopWave();
            uiManager.SetGameOver();
            SceneManager.LoadScene("MainScene");
        }
        public void IsClear()
        {

            if (!clear)
            {
                switch (difficulty)

                {
                    case 0://easy
                        if (currentWaveIndex >= 3)
                        {
                            Debug.Log("클리어");
                            clear = true;
                            GameClear?.Invoke();
                            MiniGameManager.Instance.StageClear?.Invoke(2, 1);//결합도 문제
                        }
                        break;
                    case 1://normal
                        if (currentWaveIndex >= 5)
                        {
                            Debug.Log("클리어");
                            clear = true;
                            GameClear?.Invoke();
                            MiniGameManager.Instance.StageClear?.Invoke(2, 2);//결합도 문제
                        }
                        break;
                    case 2://hard

                        if (currentWaveIndex >= 7)
                        {
                            Debug.Log("클리어");
                            clear = true;
                            GameClear?.Invoke();
                            MiniGameManager.Instance.StageClear?.Invoke(2, 3);//결합도 문제
                        }
                        break;
                    case 3://challenge

                        if (currentWaveIndex >= 10)
                        {
                            Debug.Log("클리어");
                            clear = true;
                            GameClear?.Invoke();
                            MiniGameManager.Instance.StageClear?.Invoke(2, 4);//결합도 문제
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
