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
            if(ShareManager.Instance != null)
                text.text = ShareManager.Instance.countDown.ToString();
        }
    }
}