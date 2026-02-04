using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public CheckPointMessage messageUI;
    public int key = 01;
    private string checkPointKey = "CheckPointKey";
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            VerifyCheckPoint();
        }
    }

    void VerifyCheckPoint()
    {
        TurnItOn();
        SaveCheckPoint();
    }

    void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.cyan);
        messageUI.ShowMessage("CheckPoint Activated!");
    }

    void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.gray);
    }
    
    void SaveCheckPoint()
    {
        if(PlayerPrefs.GetInt(checkPointKey, 0) > key)
            PlayerPrefs.SetInt(checkPointKey, key);

        CheckPointManager.Instance.SaveCheckPoint(key);
    }
}
