using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Button _button;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private RectTransform _buttonRectTransform;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private float _scalingDuration = 0.35f;
    
    /// <summary>
    /// 
    /// </summary>
    private Tween _sequence;
    
    private void Start()
    {
        _button
            .OnPointerEnterAsObservable()
            .Subscribe(_=> StartButtonAnimation())
            .AddTo(this.gameObject);
        
        _button
            .OnPointerExitAsObservable()
            .Subscribe(_=> FinishButtonAnimation())
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Quite()
    {
#if UNITY_WEBGL
        Application.ExternalEval("window.open('','_self','');");
        Application.ExternalEval("window.close();");
        
#else
        Application.Quit();
#endif
    }
    
    /// <summary>
    /// 
    /// </summary>
    private void StartButtonAnimation()
    {
        _sequence = DOTween.Sequence()
            .Append(_buttonRectTransform.DOScale(1.1f, _scalingDuration))
            .Append(_buttonRectTransform.DOScale(1.0f, _scalingDuration))
            .SetEase(Ease.Linear)
            .SetLoops(-1)
            .SetLink(this.gameObject);

        _sequence.Restart();
    }
    
    /// <summary>
    /// 
    /// </summary>
    private void FinishButtonAnimation()
    {
        _buttonRectTransform.DOScale(1.0f, _scalingDuration);
        _sequence.Pause();
    }
}
