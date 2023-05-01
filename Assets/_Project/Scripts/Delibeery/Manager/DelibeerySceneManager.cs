using PorfirioPartida.Delibeery.Common;
using UnityEngine.SceneManagement;

namespace PorfirioPartida.Delibeery.Manager
{
    public class DelibeerySceneManager : Singleton<DelibeerySceneManager>
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}