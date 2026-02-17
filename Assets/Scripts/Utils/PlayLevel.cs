using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    void Awake()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.FileLoaded += OnLoad;
        }
    }

    public void OnLoad(SaveSetup setup)
    {
        messageText.text = "Play" + setup.lastLevel;
    }

    void OnDestroy()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.FileLoaded -= OnLoad;
        }
    }
}
