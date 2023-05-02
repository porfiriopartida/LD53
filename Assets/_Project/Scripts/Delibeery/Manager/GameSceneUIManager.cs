using PorfirioPartida.Delibeery.Common;
using UnityEngine;
using UnityEngine.UI;

namespace PorfirioPartida.Delibeery.Manager
{
    public class GameSceneUIManager : Singleton<GameSceneUIManager>
    {
        public Button restartButton;
        
        public TMPro.TMP_Text honeyCounterText;
        public TMPro.TMP_Text beesCounterText;
        
        public FloatValue totalHoney;
        public FloatValue beeCounter;

        public void UpdateHoney()
        {
            var honeyCounterRounded = totalHoney.value;
            var fc = Mathf.Round(honeyCounterRounded * 100f) / 100f;
            honeyCounterText.text = $"{fc}";
        }

        public void UpdateBees()
        {
            beesCounterText.text = $"{beeCounter.value}";
        }

        private void Start()
        {
            restartButton.onClick.AddListener(RestartButtonPressed);
        }

        private static void RestartButtonPressed()
        {
            DelibeerySceneManager.Instance.Restart();
        }

        public void UpdateAll()
        {
            UpdateBees();
            UpdateHoney();
        }
    }
}