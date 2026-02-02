using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;

public class UIRestartInstructions : MonoBehaviour
{
    public HealthBase healthBase;
    public TextMeshProUGUI healthText;

    void Update()
    {
        RespawnInstructions();
    }

    void RespawnInstructions()
    {
        if (healthBase.isDead)
        {
            healthText.text = "Press R to Restart!";
        }
        else healthText.text = "";
    }
}
