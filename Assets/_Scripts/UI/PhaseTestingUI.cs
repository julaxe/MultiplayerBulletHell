using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.UI
{
    public class PhaseTestingUI : MonoBehaviour
    {
        public void ChangePhasePressed()
        {
            Managers.Player.Instance.SwitchBetweenShootingAndSpawning(GameManager.Instance.PlayState);
        }

    }
}