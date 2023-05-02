using PorfirioPartida.Delibeery.Common;
using UnityEngine;
using UnityEngine.UI;

namespace PorfirioPartida.Delibeery.Manager
{
    public class GameSceneUIManager : Singleton<GameSceneUIManager>
    {
        public Button restartButton;
        public Transform gameOverPanel;
        //public string gameOverTextFormat;

        public TMPro.TMP_Text honeyCounterText;
        public TMPro.TMP_Text beesCounterText;
        public TMPro.TMP_Text deadCounterText;
        public TMPro.TMP_Text gameOverText;
        
        public FloatValue totalHoney;
        public FloatValue beeCounter;
        public FloatValue deadCounter;

        private void Start()
        {
            gameOverPanel.gameObject.SetActive(false);
            restartButton.onClick.AddListener(RestartButtonPressed);
        }
        

        public void UpdateHoney()
        {
            var honeyCounterRounded = totalHoney.value;
            var fc = Mathf.Round(honeyCounterRounded * 10f) / 10f;
            honeyCounterText.text = $"{fc}";
        }

        public void UpdateBees()
        {
            beesCounterText.text = $"{beeCounter.value}";
        }

        private static void RestartButtonPressed()
        {
            DelibeerySceneManager.Instance.Restart();
        }

        public void UpdateAll()
        {
            UpdateBees();
            UpdateHoney();
            UpdateDead();
        }

        public void UpdateDead()
        {
            deadCounterText.text = $"{deadCounter.value}";
        }
        public void GameOver(string reason)
        {
            gameOverText.text = string.Format(gameOverText.text, reason);
            gameOverPanel.gameObject.SetActive(true);
        }
    }
}