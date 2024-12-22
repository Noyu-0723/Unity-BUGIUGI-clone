using System;
using UnityEngine;

[Serializable]
public class SoundEffectData
{
    /// <summary>
    /// BGMの種類
    /// </summary>
    public enum SoundEffect
    {
        PlayerDie,
        EnemyDamage,
        PlayerAttack,
        SwordSwinging,
        Explosion,
        Switching,
        Clear,
        Lose,
        Decision,
        Cancel,
    }

    /// <summary>
    /// BGMの種類
    /// </summary>
    [Tooltip("音の種類をラベルで設定")]
    public SoundEffect soundEffect;
    
    /// <summary>
    /// 音のファイル
    /// </summary>
    [Tooltip("使用したい音を設定")]
    public AudioClip audioClip;
    
    /// <summary>
    /// 音量
    /// </summary>
    [Range(0.0f, 1.0f), Tooltip("音量")] public float volume = 1.0f;
}
