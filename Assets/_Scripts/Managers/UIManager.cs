using System.Collections.Generic;
using System.Linq;
using _Scripts.Utilities;
using UnityEngine;

namespace _Scripts.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private Canvas lobbyCanvas;
        [SerializeField] private Canvas countDownCanvas;
        [SerializeField] private Canvas playerCanvas;
        [SerializeField] private Canvas spawningCanvas;
        [SerializeField] private Canvas phaseTestingCanvas;

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
            //initialize countdown
        }

        public void ShowSpawningUI()
        {
            ShowCanvas(playerCanvas, spawningCanvas, phaseTestingCanvas);
        }

        public void ShowShootingUI()
        {
            ShowCanvas(playerCanvas, phaseTestingCanvas);
        }

        public Canvas GetSpawningCanvas() => spawningCanvas;
    }
}