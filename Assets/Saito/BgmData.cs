using System;
using UnityEngine;

[Serializable]
public class BgmData
{
    /// <summary>
    /// BGMの種類
    /// </summary>
    public enum BGM
    {
        Title,
        InGame,
        Result
    }

    /// <summary>
    /// BGMの種類
    /// </summary>
    [Tooltip("音の種類をラベルで設定")] public BGM bgm;

    /// <summary>
    /// 音のファイル
    /// </summary>
    [Tooltip("使用したい音を設定")] public AudioClip audioClip;

    /// <summary>
    /// 音量
    /// </summary>
    [Range(0.0f, 1.0f), Tooltip("音量")] public float volume = 1.0f;
}