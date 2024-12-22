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

    int num;

    private void Start()
    {
        _timerManager
            .CountDownTime
            .Subscribe(_view.SetView)
            .AddTo(this.gameObject);

        num = 0;
    }

	private void Update()
	{
       // if(GameController.instance.isGameStart)
		{
            if(num <= 0)
			{
                num = 1;
                //_timerManager.StartBattleCountTime();
            }
        }
    }
}
