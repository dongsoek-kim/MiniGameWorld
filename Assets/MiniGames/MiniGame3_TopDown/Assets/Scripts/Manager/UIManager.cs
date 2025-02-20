using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TopDown
{

    public class UIManager : MonoBehaviour
    {



        [SerializeField] public TextMeshProUGUI waveText;
        [SerializeField] public Slider hpSlider;
        public TextMeshProUGUI Text;

        private void Start()
        {
            ChangePlayerHP(1, 1);
            if (Text == null)
                    Text.gameObject.SetActive(false);
        }

        public void SetGameOver()
        {
            Text.text = "Game Over";
            Text.gameObject.SetActive(true);
        }
        public void SetGameClear()
        {
            Text.text = "Game Clear";
            Text.gameObject.SetActive(true);
        }

        public void ChangeWave(int waveIndex)
        {
            waveText.text = waveIndex.ToString();
        }

        public void ChangePlayerHP(float currentHP, float maxHP)
        {
            hpSlider.value = currentHP / maxHP;
        }

    }
}