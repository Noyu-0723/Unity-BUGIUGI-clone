using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// AnimationのUtility
/// </summary>
public static class AnimationUtility
{
    /// <summary>
    /// フェードインのTween
    /// </summary>
    public static Tween FadeInCanvasGroupTween(CanvasGroup canvas,float duration= 2.5f)
    {
        return canvas.DOFade(1.0f, duration)
            .SetEase(Ease.Linear);
    }
    
    /// <summary>
    /// フェードインのTween
    /// </summary>
    public static Tween FadeOutCanvasGroupTween(CanvasGroup canvas,float duration= 2.5f)
    {
        return canvas.DOFade(0.0f, duration)
            .SetEase(Ease.Linear);
    }
}