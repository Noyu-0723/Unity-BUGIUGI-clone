using UniRx;
using UniRx.Triggers;
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
        ObservableStateMachineTrigger trigger =
            _animator.GetBehaviour<ObservableStateMachineTrigger>();
        
        trigger
            .OnStateExitAsObservable()
            .Subscribe(onStateInfo =>
            {
                if (onStateInfo.StateInfo.IsName("Base Layer.Mushroom_Die"))
                {
                 Destroy(this.gameObject);   
                }
            }).AddTo(this);
        
        _shieldEnemy
            .OnAttack
            .Subscribe(_=> _animator.SetTrigger("AttackTrigger"))
            .AddTo(this.gameObject);

        _shieldEnemy
            .OnDie
            .Subscribe(_=>_animator.SetTrigger("DieTrigger"))
            .AddTo(this.gameObject);
    }
}
