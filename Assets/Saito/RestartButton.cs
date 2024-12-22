using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Button _restartButton;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private AudioManager _audioManager;
    
    private void Start()
    {
        _restartButton
            .OnClickAsObservable()
            .Subscribe(_=>
            {
                _audioManager.PlaySoundEffect(SoundEffectData.SoundEffect.Decision);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            })
            .AddTo(this.gameObject);
    }
}
