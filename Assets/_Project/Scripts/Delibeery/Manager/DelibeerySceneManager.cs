using System.Collections;
using PorfirioPartida.Delibeery.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PorfirioPartida.Delibeery.Manager
{
    public class DelibeerySceneManager : Singleton<DelibeerySceneManager>
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void RestartAfter(int seconds)
        {
            StartCoroutine(RestartAfterSeconds(seconds));
        }

        private IEnumerator RestartAfterSeconds(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            Restart();
        }
    }
}