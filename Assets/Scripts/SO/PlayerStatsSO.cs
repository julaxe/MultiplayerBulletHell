using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "Player/PlayerStats", order = 0)]
    public class PlayerStatsSO : ScriptableObject
    {
        public int maxHealth = 100;
        public float speed = 5.0f;
        public float maxRotation = 30.0f;
        [Range(0.0f, 1.0f)]
        public float accRotation = 0.1f;
        
        [Range(0.0f,1.0f)]
        public float acceleration = 0.15f;
        public WeaponSO weapon;
    }
}