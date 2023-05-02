using PorfirioPartida.Delibeery.Manager;
using UnityEngine;

namespace PorfirioPartida.Delibeery.Player
{
    public class BeeGps : MonoBehaviour
    {
        private void Start()
        {
            BeeManager.Instance.AddBee(this);
        }

        public void Dispose()
        {
            BeeManager.Instance.RemoveBee(this);
        }
    }
}