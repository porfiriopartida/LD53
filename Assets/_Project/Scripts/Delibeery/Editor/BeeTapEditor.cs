using PorfirioPartida.Delibeery.Player;
using UnityEditor;
using UnityEngine;

namespace PorfirioPartida.Beeditor.Delibeery
{
    [CustomEditor(typeof(Bee))]
    [CanEditMultipleObjects]
    public class BeeTapEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            if (GUILayout.Button("Toggle Direction"))
            {
                foreach (var t in targets)
                {
                    var beeTap = (Bee) t;
                    beeTap.ToggleDirection();
                }
            }
            if (GUILayout.Button("Die"))
            {
                foreach (var t in targets)
                {
                    var beeTap = (Bee) t;
                    beeTap.Die();
                }
            }
            if (GUILayout.Button("Interact"))
            {
                foreach (var t in targets)
                {
                    var beeTap = (Bee) t;
                    beeTap.Interact();
                }
            }
            if (GUILayout.Button("ResumeFlying"))
            {
                foreach (var t in targets)
                {
                    var beeTap = (Bee) t;
                    beeTap.ResumeFlying();
                }
            }
            DrawDefaultInspector();
        }
    }
}