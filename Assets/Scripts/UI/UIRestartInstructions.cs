using System.Collections;
using TMPro;
using UnityEngine;

public class UIRestartInstructions : MonoBehaviour
{
    public HealthBase healthBase;
    public TextMeshProUGUI instructionsText;
    public string uiInstructions;
    

    void Update()
    {
        HealthRespawnInstructions();
    }

    void HealthRespawnInstructions()
    {
        if (healthBase != null && healthBase.isDead)
        {
            instructionsText.enabled = true;
            instructionsText.text = uiInstructions;
        }
        else if (healthBase != null && !healthBase.isDead)
        {
            instructionsText.enabled = false;
        }
    }
}
