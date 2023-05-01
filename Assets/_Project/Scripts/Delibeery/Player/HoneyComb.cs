using PorfirioPartida.Delibeery.Common;
using UnityEngine;

namespace PorfirioPartida.Delibeery.Player
{
    public class HoneyComb : MonoBehaviour, IInteractable
    {
        public GameObject beePrefab;
        public FloatValue totalHoneyComb;
        public float beeCost;
        public Transform beeStorage;
        public Transform origin;
        public void Interact()
        {
            if (totalHoneyComb.value >= beeCost)
            {
                Instantiate(beePrefab, origin.transform.position, Quaternion.identity, beeStorage);
            }
        }
    }
}