using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FlappyPlane
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI restartText;
        // Start is called before the first frame update
        void Start()
        {
            if (restartText == null)
                if (scoreText == null)
                    restartText.gameObject.SetActive(false);
        }

        public void SetRestart()
        {
            restartText.gameObject.SetActive(true);
        }
        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}