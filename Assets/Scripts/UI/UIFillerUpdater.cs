using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum UIUpdaterType
{
    Health,
    Gun
}

public class UIFillerUpdater : MonoBehaviour
{
    public Image uiImage;
    public UIUpdaterType updaterType = UIUpdaterType.Health;

    [Header("Animation")]
    public float duration = .1f;
    public Ease ease = Ease.OutBack;
    
    private Tween _currentTween;
    
    public void OnValidate()
    {
        if (uiImage == null) uiImage = GetComponent<Image>();
    }
    
    public void UpdateValue(float value)
    {
        uiImage.fillAmount = value;
    }

    public void UpdateValue(float max, float current)
    {
        if (_currentTween != null) _currentTween.Kill();
        uiImage.DOFillAmount(1 - (current / max), duration).SetEase(ease);
    }
}
