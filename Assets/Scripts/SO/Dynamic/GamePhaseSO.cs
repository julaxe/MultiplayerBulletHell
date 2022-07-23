using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace SO
{
    [CreateAssetMenu(fileName = "GamePhaseSO", menuName = "Game/GamePhase", order = 0)]
    public class GamePhaseSO : ScriptableObject
    {
        public enum Phase
        {
            Shooting = 0,
            Spawning
        }

        public Phase currentPhase = Phase.Shooting;
        public UnityEvent phaseChanged;

        public void ChangePhase()
        {
            int intPhase = (int) currentPhase;
            var nextPhase = (intPhase + 1) % 2;
            currentPhase = (Phase) nextPhase;
            phaseChanged?.Invoke();
        }

        
        
        
    }
}