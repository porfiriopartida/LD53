using PorfirioPartida.Delibeery.Common;

namespace PorfirioPartida.Delibeery.Manager
{
    public class LebeelManager : Singleton<LebeelManager>
    {
        public FloatValue totalHoney;

        private void Start()
        {
            totalHoney.value = 0;
        }
    }
}