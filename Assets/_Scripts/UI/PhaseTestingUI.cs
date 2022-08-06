using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.UI
{
    public class PhaseTestingUI : MonoBehaviour
    {
        public void ChangePhasePressed()
        {
            ChangePhase_ServerRpc();
        }
        
        private void ChangePhase_ServerRpc()
        {
            ChangePhase_ClientRpc();
        }
        
        private void ChangePhase_ClientRpc()
        {
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