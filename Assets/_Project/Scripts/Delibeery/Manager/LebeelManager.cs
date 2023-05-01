using PorfirioPartida.Delibeery.Common;
using UnityEngine;

namespace PorfirioPartida.Delibeery.Manager
{
    public class LebeelManager : Singleton<LebeelManager>
    {
        public FloatValue totalHoney;
        public Transform flowerTransform;

        private void Start()
        {
            totalHoney.value = 0;
        }
    }
}