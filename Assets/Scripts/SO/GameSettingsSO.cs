using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game/GameSettingsSO", order = 0)]
    public class GameSettingsSO : ScriptableObject
    {
        public float screenHeight = 10f;
        public float screenWidth = 5.6f;
    }
}