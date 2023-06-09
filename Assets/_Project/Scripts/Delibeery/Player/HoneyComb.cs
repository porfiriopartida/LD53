﻿using PorfirioPartida.Delibeery.Common;
using PorfirioPartida.Delibeery.Manager;
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
        public HoneyCombFragment fragments;
        public void Interact()
        {
            TrySpawnBee();
        }

        public void TrySpawnBee()
        {
            if (totalHoneyComb.value < beeCost)
            {
                return;
            }
            Instantiate(beePrefab, origin.transform.position, Quaternion.identity, beeStorage);
            TakeHoney(beeCost);
        }

        public void TakeHoney(float honey)
        {
            totalHoneyComb.value -= honey;
            GameSceneUIManager.Instance.UpdateHoney();
        }

        public int spawnThreshold;
        public void AddHoney(float honey)
        {
            totalHoneyComb.value += honey;
            GameSceneUIManager.Instance.UpdateHoney();
            if (totalHoneyComb.value >= spawnThreshold)
            {
                TrySpawnBee();
            }
        }

        public void SetHoney(float honey)
        {
            totalHoneyComb.value = honey;
            GameSceneUIManager.Instance.UpdateHoney();
        }
    }
}