using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class ShieldEnemy : Enemy
{
   /// <summary>
   /// 
   /// </summary>
   [SerializeField] private Collider2D _shieldCollider2D;
   
   /// <summary>
   /// 
   /// </summary>
   [SerializeField] private Collider2D _backCollider2D;
   
   private void Start()
   {
      Observable
         .EveryFixedUpdate()
         .Subscribe(_=> Move())
         .AddTo(this.gameObject);

      //ここで音の再生とか
      _shieldCollider2D
         .OnTriggerEnterAsObservable()
         .Subscribe()
         .AddTo(this.gameObject);
      
      /*
      _backCollider2D
         .OnTriggerEnterAsObservable()
         .Subscribe(_=> Dead())
         .AddTo(this.gameObject);
   */
   }
}