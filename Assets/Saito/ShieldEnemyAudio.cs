using UniRx;
using UnityEngine;

public class ShieldEnemyAudio : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private ShieldEnemyController _enemyController;
    
    /// <summary>
    /// 
    /// </summary>
    private AudioManager _audioManager;

    private void Start()
    {
        //やりたくないね
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        _enemyController
            .OnDie
            .Subscribe(_=>_audioManager.PlaySoundEffect(SoundEffectData.SoundEffect.EnemyDamage))
            .AddTo(this.gameObject);
    }
}
