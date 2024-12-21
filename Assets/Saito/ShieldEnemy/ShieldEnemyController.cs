using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class ShieldEnemyController : Enemy
{
   public IObservable<Unit> OnAttack => _attackSubject;
   private Subject<Unit> _attackSubject = new Subject<Unit>();
   
   /// <summary>
   /// 
   /// </summary>
   [SerializeField] private Collider2D _shieldCollider2D;
   
   /// <summary>
   /// 
   /// </summary>
   [SerializeField] private Collider2D _backCollider2D;
   
   /*public override void Start()
   {
      //base.Start();
      
      _shieldCollider2D
         .OnCollisionEnter2DAsObservable()
         .Subscribe(_=>_attackSubject.OnNext(Unit.Default))
         .AddTo(this.gameObject);
      
      _backCollider2D
         .OnCollisionEnter2DAsObservable()
         .Where(x=> x.gameObject.CompareTag("Player"))
          .Where(x=>x.gameObject.transform.forward == this.gameObject.transform.forward)
         .Subscribe(_=> Destroy(this.gameObject))
         .AddTo(this.gameObject);
   }*/

   /*
   public override void OnCollisionEnter2D(Collision other)
   {
      // do nothing
   }
   */
}