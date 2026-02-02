using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPointKey = 0;
    public List<CheckPointBase> checkPoints;
    public MeshRenderer meshRenderer;
    public Material material;
    
    public bool HasCheckPoint()
    {
        return lastCheckPointKey > 0;
    }
    public void SaveCheckPoint(int i)
    {
        if(i > lastCheckPointKey)
            lastCheckPointKey = i;
        
        
    }

    public Vector3 GetLastCheckPoint()
    {
        var checkPoint = checkPoints.Find(i => i.key == lastCheckPointKey);
        return checkPoint.transform.position;
    }
}
