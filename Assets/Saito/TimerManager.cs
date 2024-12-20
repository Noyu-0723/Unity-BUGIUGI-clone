using System;
using UniRx;
using UnityEngine;

/// <summary>
/// 時間を管理する
/// </summary>
public class TimerManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public IReactiveProperty<float> CountDownTime => _countDownTime;
    [SerializeField] private FloatReactiveProperty _countDownTime = new FloatReactiveProperty();
    
   /// <summary>
   /// 
   /// </summary>
    private float _battleTime;

    private void Start()
    {
        _battleTime = _countDownTime.Value;
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void StartBattleCountTime()
    {
        Observable
            .Timer(TimeSpan.FromSeconds(0.0f), TimeSpan.FromSeconds(0.01f))
            .Select(x => (_battleTime - x * 0.01f))
            .TakeWhile(x => x >= 0)
            .Subscribe(x => _countDownTime.Value = x)
            .AddTo(this.gameObject);
    }
}