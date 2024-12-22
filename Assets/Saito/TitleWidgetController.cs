using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TitleWidgetController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private CanvasGroup _resultCanvasGroup;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] float _hideDuration = 0.5f;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Button _startButton;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private AudioManager _audioManager;
    
    private void Start()
    {
        _audioManager.PlayBgm(BgmData.BGM.Title);
        
        _startButton
            .OnClickAsObservable()
            .Subscribe(_=>
            {
                _audioManager.PlaySoundEffect(SoundEffectData.SoundEffect.Decision);
                _audioManager.StopBgm();
                _audioManager.PlayBgm(BgmData.BGM.InGame);
                Hide();
            })
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Hide()
    {
        AnimationUtility.FadeOutCanvasGroupTween(_resultCanvasGroup,_hideDuration);
        _resultCanvasGroup.blocksRaycasts = false;
        _resultCanvasGroup.interactable = false;
    }
}
