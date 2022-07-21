using UnityEngine;
using UnityEngine.Events;

namespace SO
{
    [CreateAssetMenu(fileName = "NetworkSO", menuName = "Network/NetworkSo", order = 0)]
    public class NetworkSO : ScriptableObject
    {
        public bool isPlayer1 = false;
        public PlayerInfo player1Info;
        public PlayerInfo player2Info;
    }
}