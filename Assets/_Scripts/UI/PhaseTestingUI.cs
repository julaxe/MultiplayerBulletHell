using _Scripts.Managers;
using Unity.Netcode;

namespace _Scripts.UI
{
    public class PhaseTestingUI : NetworkBehaviour
    {
        public void ChangePhasePressed()
        {
            ChangePhase_ServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void ChangePhase_ServerRpc()
        {
            ChangePhase_ClientRpc();
        }

        [ClientRpc]
        private void ChangePhase_ClientRpc()
        {
            print("Change State BROOOO");
            if (GameManager.Instance.State == GameState.Shooting)
            {
                GameManager.Instance.ChangeState(GameState.Spawning);
            }
            else if (GameManager.Instance.State == GameState.Spawning)
            {
                GameManager.Instance.ChangeState(GameState.Shooting);
            }
        }
        
    }
}