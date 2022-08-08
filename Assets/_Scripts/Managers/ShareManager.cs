using FishNet.Object;
using FishNet.Object.Synchronizing;


namespace _Scripts.Managers
{
    public class ShareManager : NetworkBehaviour
    {
        public static ShareManager Instance { get; private set; }

        [SyncVar] public int countDown = 0;

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            Instance = this;
        }

        public void DecreaseCountdown(int value)
        {
            if (countDown == 0) return;

            countDown -= value;
            if (countDown < 0) countDown = 0;
        }

        public void InitializeCountdown()
        {
            countDown = 4;
        }
        
        
        
    }
}