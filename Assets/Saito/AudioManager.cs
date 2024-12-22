using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private AudioComponent _audioComponent;

    /// <summary>
    /// 
    /// </summary>
    public void PlayBgm(BgmData.BGM bgm)
    {
        _audioComponent.PlayBgm(bgm);
    }

    /// <summary>
    /// 
    /// </summary>
    public void StopBgm()
    {
        _audioComponent.StopBgm();
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void PlaySoundEffect(SoundEffectData.SoundEffect soundEffect)
    {
        _audioComponent.PlaySoundEffect(soundEffect);
    }
}
