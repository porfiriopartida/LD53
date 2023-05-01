using System.Collections.Generic;
using UnityEngine;

namespace PorfirioPartida.Delibeery.Effect
{
    public class DestroyHandler : MonoBehaviour
    {
        public Vector2 randomSecondsRange;
        
        [SerializeField]
        [Header("Reset on Start fields:")]
        private float remainingLifeTime;
        private void Start()
        {
            remainingLifeTime = Random.Range(randomSecondsRange.x, randomSecondsRange.y);
        }

        private void Update()
        {
            remainingLifeTime -= Time.deltaTime;
            if (remainingLifeTime < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}