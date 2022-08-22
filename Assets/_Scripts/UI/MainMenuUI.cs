using System;
using _Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button singlePlayerButton;
        [SerializeField] private Button multiplayerButton;
        [SerializeField] private Button storeButton;
        [SerializeField] private Button creditsButton;

        private void Start()
        {
            singlePlayerButton.onClick.AddListener(() =>
            {
                GameManager.Instance.ChangeState(GameState.StageMenu);
            });
            multiplayerButton.onClick.AddListener(() =>
            {
                GameManager.Instance.ChangeState(GameState.Multiplayer);
            });
            storeButton.onClick.AddListener(() =>
            {
                GameManager.Instance.ChangeState(GameState.Store);
            });
            creditsButton.onClick.AddListener(() =>
            {
                GameManager.Instance.ChangeState(GameState.Credits);
            });
        }
    }
}