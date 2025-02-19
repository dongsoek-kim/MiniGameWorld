using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FlappyPlane
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI Text;
        // Start is called before the first frame update
        void Start()
        {
            if (Text == null)
                if (scoreText == null)
                    Text.gameObject.SetActive(false);
        }

        public void SetGameOver()
        {
            Text.text ="Game Over";
            Text.gameObject.SetActive(true);
        }
        public void SetGameClear()
        {
            Text.text = "Game Clear";
            Text.gameObject.SetActive(true);
        }
        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}