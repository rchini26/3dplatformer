using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = .1f;
    public string colorParameter = "_EmissionColor";
    
    private Color _defaultColor;
    private Tween _currentTween;

    private void Start()
    {
        _defaultColor = meshRenderer.material.GetColor(colorParameter);
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (!_currentTween.IsActive())
        {
            _currentTween = meshRenderer.material.DOColor(color, colorParameter, duration).SetLoops(2, LoopType.Yoyo);   
        }
    }
}
