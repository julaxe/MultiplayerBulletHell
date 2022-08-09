using System.Globalization;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI healthText;
        [SerializeField] private TMPro.TextMeshProUGUI scoreText;

        private void Update()
        {
            if (Player.Instance == null) return;
            scoreText.text = Player.Instance.score.ToString();
            healthText.text = Player.Instance.hp.ToString();
        }
    }
}
