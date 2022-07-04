using UnityEngine;
using UnityEngine.Events;

namespace SO
{
    [CreateAssetMenu(fileName = "PlayerHealthSO", menuName = "Player/PlayerHealth", order = 0)]
    public class PlayerHealthSO : ScriptableObject
    {
        public int currentHealth;

        public UnityEvent takeDamageEvent;

        public void TakeDamage(int damage)
        {
            if(currentHealth == 0) return;

            currentHealth -= damage;
            if (currentHealth < 0) currentHealth = 0;
            
            takeDamageEvent?.Invoke();
        }
    }
}