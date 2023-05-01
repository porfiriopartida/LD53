using System;
using PorfirioPartida.Delibeery.Common;
using UnityEngine.UI;

namespace PorfirioPartida.Delibeery.Manager
{
    public class GameSceneUIManager : Singleton<GameSceneUIManager>
    {
        public Button restartButton;
        public TMPro.TMP_Text text;
        public FloatValue totalHoney;

        private void Update()
        {
            text.text = $"{totalHoney.value}";
        }

        private void Start()
        {
            restartButton.onClick.AddListener(RestartButtonPressed);
        }

        private static void RestartButtonPressed()
        {
            DelibeerySceneManager.Instance.Restart();
        }
    }
}