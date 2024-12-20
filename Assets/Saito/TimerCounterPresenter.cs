using UniRx;
using UnityEngine;

public class TimerCounterPresenter : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private TimerManager _timerManager;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [SerializeField] private TimerCounterView _view;
    
    private void Start()
    {
        _timerManager.StartBattleCountTime();
      
        _timerManager
            .CountDownTime
            .Subscribe(_view.SetView)
            .AddTo(this.gameObject);
    }
}
