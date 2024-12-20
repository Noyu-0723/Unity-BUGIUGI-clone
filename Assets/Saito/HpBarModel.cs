using UniRx;

public class HpBarModel
{
    /// <summary>
    /// 
    /// </summary>
    public IReactiveProperty<int> HpProp => _hpProp;
    private ReactiveProperty<int> _hpProp;

    public HpBarModel(int hp)
    {
        _hpProp = new ReactiveProperty<int>(hp);   
    }
    
    public void SetHp(int hp)
    {
        _hpProp.Value = hp;
    }
}
