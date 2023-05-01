using PorfirioPartida.Delibeery.Manager;
using UnityEngine;

namespace PorfirioPartida.Delibeery.Player
{
    public class BeeGps : MonoBehaviour
    {
        private void Start()
        {
            BeeManager.Instance.beeList.Add(this);
        }

        public void Dispose()
        {
            BeeManager.Instance.beeList.Remove(this);
        }
    }
}