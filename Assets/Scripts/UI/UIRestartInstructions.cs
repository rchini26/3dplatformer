using System.Collections;
using TMPro;
using UnityEngine;

public class UIRestartInstructions : MonoBehaviour
{
    public HealthBase healthBase;
    public TextMeshProUGUI instructionsText;
    public SOInt soInt;
    public string uiInstructions;
    public float displayDuration = 2f;

    private bool _hasShownInstructions;

    void Update()
    {
        HealthRespawnInstructions();
        FirstLifePackInstructions();
    }

    void HealthRespawnInstructions()
    {
        if (healthBase != null && healthBase.isDead)
        {
            instructionsText.text = uiInstructions;
        }
    }

    void FirstLifePackInstructions()
    {
        if (soInt != null && soInt.value > 0 && !_hasShownInstructions)
        {
            _hasShownInstructions = true; 
            StartCoroutine(ShowInstructionsTemporarily());
        }
    }

    IEnumerator ShowInstructionsTemporarily()
    {
        instructionsText.text = uiInstructions;
        
        yield return new WaitForSeconds(displayDuration);
        
        instructionsText.enabled = false;
    }
}
