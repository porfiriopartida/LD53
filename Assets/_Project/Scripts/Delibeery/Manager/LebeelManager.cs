using PorfirioPartida.Delibeery.Common;
using PorfirioPartida.Delibeery.Player;
using UnityEngine;

namespace PorfirioPartida.Delibeery.Manager
{
    public class LebeelManager : Singleton<LebeelManager>
    {
        public float startingHoney;
        
        public FloatValue totalHoney;
        public FloatValue beeCounter;

        public Transform flowerTransform;

        public HoneyComb HoneyComb;
        
        private void Start()
        {
            GameSceneUIManager.Instance.UpdateAll();
            HoneyComb.SetHoney(startingHoney);
            beeCounter.value = 0;
        }
    }
}