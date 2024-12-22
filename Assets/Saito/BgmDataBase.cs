using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// BGMを管理する
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/BgmDataBase")]
public class BgmDataBase : ScriptableObject
{
    /// <summary>
    /// BGMを管理するリスト
    /// </summary>
    [SerializeField]
    List<BgmData> _BgmDataList = new List<BgmData>();

    /// <summary>
    /// BGMを取得する
    /// </summary>
    /// <param name="bgm">取得したいBGM</param>
    /// <returns></returns>
    public BgmData GetBgmData(BgmData.BGM bgmData)
    {
        var bgm = _BgmDataList.FirstOrDefault(clip => clip.bgm == bgmData);
        return bgm;
    }
}