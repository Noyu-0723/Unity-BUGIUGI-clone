using UnityEngine.UI;
using UnityEngine;

public class HpBarView : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Image _hpBarImage;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private int _hpScale = 10;
    
    private void Start()
    {
        _hpBarImage.fillAmount = 1.0f;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hp"></param>
    public void SetView(int hp)
    {
        if (hp < 0)
            return;
        
        _hpBarImage.fillAmount = hp / (float)_hpScale;
    }
}
