using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class HpBarPresenter : MonoBehaviour
{
    [SerializeField] private Button _damageButton;
    
    /// <summary>
    /// 
    /// </summary>
    private HpBarModel _model;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private HpBarView _view;
    
    private void Awake()
    {
        _model = new HpBarModel(10);
    }
    
    private void Start()
    {
        int hp = 10;
        
        _damageButton
            .OnClickAsObservable()
            .Subscribe(_=>
            {
                hp--;
                SetHp(hp);
            })
            .AddTo(this.gameObject);

        _model
            .HpProp
            .Subscribe(_view.SetView)
            .AddTo(this.gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hp"></param>
    public void SetHp(int hp)
    {
        _model.SetHp(hp);
    }
}
