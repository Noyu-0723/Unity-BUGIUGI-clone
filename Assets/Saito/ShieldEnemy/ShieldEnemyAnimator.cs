using UniRx;
using UnityEngine;

public class ShieldEnemyAnimator : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private ShieldEnemyController _shieldEnemy;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _shieldEnemy
            .OnAttack
            .Subscribe(_=> _animator.SetTrigger("AttackTrigger"))
            .AddTo(this.gameObject);
    }
}
