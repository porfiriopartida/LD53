using UnityEngine;

namespace PorfirioPartida.Delibeery.Effect
{
    public class Spinner : MonoBehaviour
    {
        public float localPi = 3.1416f; 
        public Vector3 axis;
        public int rotationSpeed;

        private void Update()
        {
            transform.Rotate(axis, rotationSpeed * Time.deltaTime * localPi);
        }
    }
}
