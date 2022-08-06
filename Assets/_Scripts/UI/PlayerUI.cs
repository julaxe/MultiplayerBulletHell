using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using _Scripts.Managers;
using SO;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI healthText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    private void Update()
    {
        if (PlayersManager.Instance.isPlayer1)
        {
           
        }
        else
        {
            
        }
    }
}
