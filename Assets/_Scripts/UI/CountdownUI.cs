using _Scripts.Managers;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class CountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void Update()
        {
            UpdateText();
        }

        
        private void UpdateText()
        {
            if(TimerManager.Instance != null)
                text.text = TimerManager.Instance.countDown.ToString();
        }
    }
}