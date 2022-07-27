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
            healthText.text = PlayersManager.Instance.player1Info.currentHealth.ToString(CultureInfo.InvariantCulture);
            scoreText.text = PlayersManager.Instance.player1Info.score.ToString(CultureInfo.InvariantCulture);
        }
        else
        {
            healthText.text = PlayersManager.Instance.player2Info.currentHealth.ToString(CultureInfo.InvariantCulture);
            scoreText.text = PlayersManager.Instance.player2Info.score.ToString(CultureInfo.InvariantCulture);
        }
    }
}
