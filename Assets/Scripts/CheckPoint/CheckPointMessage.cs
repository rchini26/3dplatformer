using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPointMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText;   
    public float displayDuration = 2f;    

    public void ShowMessage(string msg)
    {
        StartCoroutine(ShowMessageRoutine(msg));
    }

    private IEnumerator ShowMessageRoutine(string msg)
    {
        messageText.text = msg;
        messageText.enabled = true;

        yield return new WaitForSeconds(displayDuration);

        messageText.enabled = false;
    }
}
