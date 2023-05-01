using PorfirioPartida.Delibeery.Common;
using UnityEngine;

namespace PorfirioPartida.Delibeery.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        public Camera mainCamera;
        public float rayLength;
        public LayerMask interactablesMask;

        public GameObject fingerPrintPrefab;
        public Transform fingerPrintStorage;
        
        private void OnEnable()
        {
            Input.simulateMouseWithTouches = true;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                InteractFound(Input.mousePosition);
            }
        }

        private void InteractFound(Vector3 mousePosition)
        {
            var hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(mousePosition), mainCamera.transform.forward, rayLength, interactablesMask);
            if (hit.collider != null)
            {
                // Debug.Log($"Hit {hit.collider.gameObject.name}");
                var rotation = Quaternion.identity;
                rotation.z = Random.Range(fingerPrintRotationRange.x, fingerPrintRotationRange.y);
                Instantiate(fingerPrintPrefab, hit.point, rotation, fingerPrintStorage);


                var interactable = hit.collider.GetComponent<IInteractable>();
                interactable.Interact();
            }
        }

        public Vector2 fingerPrintRotationRange;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * rayLength);
        }
    }
}