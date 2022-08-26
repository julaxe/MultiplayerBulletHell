using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace _Scripts.UI
{
    public class LoginFormUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private Button loginButton;
        [SerializeField] private Button signUpButton;

        private void Start()
        {
            loginButton.onClick.AddListener(() =>
            {
                
            });
            
            signUpButton.onClick.AddListener(() =>
            {
                
            });
        }
    }
}