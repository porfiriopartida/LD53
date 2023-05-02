using PorfirioPartida.Delibeery.Common;
using PorfirioPartida.Delibeery.Player;
using UnityEngine;

namespace PorfirioPartida.Delibeery.Manager
{
    public class LebeelManager : Singleton<LebeelManager>
    {
        public float startingHoney;
        public FloatValue beeCounter;

        public Transform flowerTransform;

        public HoneyComb HoneyComb;

        public FloatValue deadCounter;
        public float deadCounterLose = 3;
        
        private void Start()
        {
            beeCounter.value = 0;
            deadCounter.value = 0;
            
            HoneyComb.SetHoney(startingHoney);
            
            GameSceneUIManager.Instance.UpdateAll();
        }

        public float GetXHoneyCombLimit()
        {
            return HoneyComb.origin.transform.position.x;
        }

        public void DieCounter()
        {
            deadCounter.value++;
            
            GameSceneUIManager.Instance.UpdateDead();
            
            if (deadCounter.value >= deadCounterLose)
            {
                GameSceneUIManager.Instance.GameOver("Too many dead bees.");
                DelibeerySceneManager.Instance.RestartAfter(2);
            }

            if (beeCounter.value == 0)
            {
                GameSceneUIManager.Instance.GameOver("All bees are gone!");
                DelibeerySceneManager.Instance.RestartAfter(2);
            }
        }
    }
}