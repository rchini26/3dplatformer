using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFirstLifePackInstructions : MonoBehaviour
{
    public TextMeshProUGUI instructionsText;
    public SOInt soInt;
    public string uiInstructions;
    public float displayDuration = 2f;

    private bool _hasShownInstructions;

    void Start()
    {
        instructionsText.enabled = false;
    }
    
    void Update()
    {
        if (soInt != null && soInt.value > 0 && !_hasShownInstructions)
        {
            StartCoroutine(ShowInstructionsTemporarily());
            _hasShownInstructions = true; 
        }
    }

    IEnumerator ShowInstructionsTemporarily()
    {
        instructionsText.enabled = true;
        instructionsText.text = uiInstructions;
        
        yield return new WaitForSeconds(displayDuration);
        
        instructionsText.enabled = false;
    }
}
