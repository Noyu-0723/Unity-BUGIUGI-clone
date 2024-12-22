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

    private GameController gc;
    private bool _running = false;

    private void Start()
    {
        _battleTime = _countDownTime.Value;
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }


    /// <summary>
    /// 
    /// </summary>
    public void StartBattleCountTime()
    {
        Observable
	        .Interval(TimeSpan.FromMilliseconds(10))
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

        if (gc.isGameStart && !_running)
        {
            StartBattleCountTime();
            _running = true;
        }
    }
}