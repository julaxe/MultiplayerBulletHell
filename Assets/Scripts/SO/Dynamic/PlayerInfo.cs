using System;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(menuName = "Player/PlayerInfo")]
    public class PlayerInfo : ScriptableObject
    {
        
        public float currentHealth;
        public float maxHealth;
        public int score;

        private void Awake()
        {
            ResetValues();
        }

        public void TakeDamage(float damage)
        {
            if (currentHealth == 0.0f) return;
            currentHealth -= damage;
            if (currentHealth < 0.0f)
                currentHealth = 0.0f;
        }
        public void ResetValues()
        {
            currentHealth = maxHealth;
            score = 0;
        }
    }
}