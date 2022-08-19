using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using _Scripts.Utilities;
using UnityEngine;
using DG.Tweening;
using Vector3 = UnityEngine.Vector3;

namespace _Scripts.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private Canvas lobbyCanvas;
        [SerializeField] private Canvas countDownCanvas;
        [SerializeField] private Canvas playerCanvas;
        [SerializeField] private Canvas spawningCanvas;
        [SerializeField] private Canvas phaseTestingCanvas;

        [SerializeField] private Transform playerHealthTransform;
        [SerializeField] private Transform playerScoreTransform;

        private List<Canvas> _canvasList = new List<Canvas>();

        void Start()
        {
            _canvasList.Add(lobbyCanvas);
            _canvasList.Add(countDownCanvas);
            _canvasList.Add(playerCanvas);
            _canvasList.Add(spawningCanvas);
            _canvasList.Add(phaseTestingCanvas);
        }

        private void ShowCanvas(params Canvas[] canvasesToShow)
        {
            foreach (var canvas in _canvasList)
            {
                canvas.enabled = canvasesToShow.Contains(canvas);
            }
        }
        
        public void ShowLobby()
        {
            ShowCanvas(lobbyCanvas);
        }

        public void ShowCountdown()
        {
            ShowCanvas(countDownCanvas);
        }

        public void ShowSpawningUI()
        {
            ShowCanvas(playerCanvas, spawningCanvas);
        }

        public void ShowShootingUI()
        {
            ShowCanvas(playerCanvas);
        }

        public Canvas GetSpawningCanvas() => spawningCanvas;

        public void AnimatePlayerHealth()
        {
            ShakeTransform(playerHealthTransform);
        }
        
        public void AnimatePlayerScore()
        {
            ShakeTransform(playerScoreTransform);
        }

        private void ShakeTransform(Transform transformToShake)
        {
            transformToShake.DOShakeScale(0.2f, 1.0f, 1,
                0.0f, true).OnComplete(() =>
            {
                transformToShake.DOScale(Vector3.one, 0.1f);
            });
        }
    }
}