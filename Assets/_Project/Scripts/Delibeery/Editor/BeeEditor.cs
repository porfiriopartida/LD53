using System;
using PorfirioPartida.Delibeery.Player;
using UnityEditor;
using UnityEngine;

namespace PorfirioPartida.Beeditor.Delibeery
{
    [CustomEditor(typeof(Bee))]
    [CanEditMultipleObjects]
    public class BeeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Interact"))
            {
                foreach (var t in targets)
                {
                    var beeTap = (Bee) t;
                    beeTap.Interact();
                }
            }
            GUILayout.Space(1);
            // base.OnInspectorGUI();
            if (GUILayout.Button("Toggle Direction"))
            {
                foreach (var t in targets)
                {
                    var beeTap = (Bee) t;
                    beeTap.ToggleDirection();
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
            GUILayout.Space(1);
            if (GUILayout.Button("Die"))
            {
                foreach (var t in targets)
                {
                    var beeTap = (Bee) t;
                    beeTap.Die();
                }
            }
            GUILayout.Space(2);

            if (Application.isPlaying)
            {
                foreach (var t in targets)
                {
                    var beeTap = (Bee) t;
                    GUILayout.Label($"{beeTap.name}:");
                    GUILayout.Label($" - rb.vel: {beeTap._rb.velocity}");
                    GUILayout.Label($" - alive: {beeTap.isAlive}");
                    GUILayout.Label($" - full: {beeTap.GetFullPct() * 100}%");
                }
            
                GUILayout.Space(2);
            }

            DrawDefaultInspector();
        }
    }
}