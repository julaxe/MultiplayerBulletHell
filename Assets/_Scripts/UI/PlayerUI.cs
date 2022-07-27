using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using SO;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI healthText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private NetworkSO networkSo;

    private void Update()
    {
        if (networkSo.isPlayer1)
        {
            healthText.text = networkSo.player1Info.currentHealth.ToString(CultureInfo.InvariantCulture);
            scoreText.text = networkSo.player1Info.score.ToString(CultureInfo.InvariantCulture);
        }
        else
        {
            healthText.text = networkSo.player2Info.currentHealth.ToString(CultureInfo.InvariantCulture);
            scoreText.text = networkSo.player2Info.score.ToString(CultureInfo.InvariantCulture);
        }
    }
}
