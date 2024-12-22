using UnityEngine;

public class AudioComponent : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private AudioSource _bgmAudioSource;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private AudioSource _soundEffectAudioSource;

    /// <summary>
    /// BGMのデータベース
    /// </summary>
    [SerializeField] private BgmDataBase _bgmDataBase;

    /// <summary>
    /// SEのデータベース
    /// </summary>
    [SerializeField] private SoundEffectDataBase _soundEffectDataBase;
    
    private void Start()
    {
        _bgmAudioSource = InitializeAudioSource(_bgmAudioSource, true);
        _soundEffectAudioSource = InitializeAudioSource(_soundEffectAudioSource, false);
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private AudioSource InitializeAudioSource(AudioSource audioSource, bool isLoop = false)
    {
        audioSource.loop = isLoop;
        audioSource.playOnAwake = false;

        return audioSource;
    }

    /// <summary>
    /// BGMを流す
    /// </summary>
    public void PlayBgm(BgmData.BGM bgm)
    {
        var bgmData = _bgmDataBase.GetBgmData(bgm);
            
        if (bgmData == null)
        {
            Debug.Log(bgm + "は見つかりません");
            return;
        }
            
        //音の調整
        _bgmAudioSource.volume = Mathf.Clamp(bgmData.volume, 0.0f, 1.0f);
            
        _bgmAudioSource.Play(bgmData.audioClip);
    }

    /// <summary>
    /// SEを流す
    /// </summary>
    /// <param name="soundEffect">流したいSE</param>
    public void PlaySoundEffect(SoundEffectData.SoundEffect soundEffect)
    {
        var soundEffectData = _soundEffectDataBase.GetSoundEffectData(soundEffect);
            
        if (soundEffectData == null)
        {
            Debug.Log(soundEffect + "は見つかりません");
            return;
        }
            
        //音の調整
        _soundEffectAudioSource.volume = Mathf.Clamp(soundEffectData.volume, 0.0f, 1.0f);
            
        _soundEffectAudioSource.PlayOneShot(soundEffectData.audioClip);
    }

    /// <summary>
    /// 
    /// </summary>
    public void StopBgm()
    {
        _bgmAudioSource.Stop();
    }
}
