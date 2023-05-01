using System.Collections.Generic;
using PorfirioPartida.Delibeery.Common;
using PorfirioPartida.Delibeery.Player;
using UnityEngine;

namespace PorfirioPartida.Delibeery.Manager
{
    public class BeeManager : Singleton<BeeManager>
    {
        public List<BeeGps> beeList;

        private void Update()
        {
            float xSum = 0;
            int count = beeList.Count;
            var arr = new BeeGps[count];
            beeList.CopyTo(arr);
            foreach (var beeItem in arr)
            {
                if (beeItem.isActiveAndEnabled)
                {
                    xSum += beeItem.transform.position.x;
                }
                else
                {
                    count--;
                }
            }

            if (xSum > 0 && count > 0)
            {
                var newPos = this.transform.position;
                newPos.x = xSum / count;
                this.transform.position = newPos;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            foreach (var beeItem in beeList)
            {
                Gizmos.DrawLine(this.transform.position, beeItem.transform.position);
            }
        }
    }
}
