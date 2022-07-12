using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "NetworkSO", menuName = "Network/NetworkSo", order = 0)]
    public class NetworkSO : ScriptableObject
    {
        public bool isPlayer1 = false;
    }
}