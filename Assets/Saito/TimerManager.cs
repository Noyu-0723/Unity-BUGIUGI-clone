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
		    .Timer(TimeSpan.FromMilliseconds(0),TimeSpan.FromMilliseconds(10), Scheduler.MainThreadIgnoreTimeScale)
	        .Do(x=>Debug.Log(x))
	        .Select(x => (_battleTime - x*0.01f))
            .TakeWhile(x => x >= 0)
            .Subscribe(x => _countDownTime.Value = x)
            .AddTo(this.gameObject);
    }

	private void Update()
	{
        //Debug.Log(_countDownTime.Value);
		if(_countDownTime.Value <= 0.1f)
		{
            GameController.instance.isGameClear = true;
		}
	}
}