using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class ShieldEnemyController : Enemy
{
   public IObservable<string> OnAttack => _attackSubject;
   private Subject<string> _attackSubject = new Subject<string>();
   
   /// <summary>
   /// 
   /// </summary>
   [SerializeField] private Collider2D _shieldCollider2D;
   
   /// <summary>
   /// 
   /// </summary>
   [SerializeField] private Collider2D _backCollider2D;
   
   public override void Start()
   {
      base.Start();

      Observable
         .EveryFixedUpdate()
         .Subscribe(_ => Move())
         .AddTo(this.gameObject);
      
      _shieldCollider2D
         .OnCollisionEnter2DAsObservable()
         .Subscribe(x=> _attackSubject.OnNext(x.collider.tag))
         .AddTo(this.gameObject);

      _attackSubject
         .Where(x=> x == "Player")
         .Subscribe(_=>AttackPlayer())
         .AddTo(this.gameObject);
      
      _attackSubject
         .Where(x=> x == "Castle")
         .Subscribe(_=>AttackCastle())
         .AddTo(this.gameObject);
      
      _backCollider2D
         .OnTriggerEnter2DAsObservable()
         .Where(x=> x.gameObject.CompareTag("Player") || x.gameObject.CompareTag("Attack"))
         .Subscribe(_=>
         {
            TakeDamage(1);
            Destroy(this.gameObject);
         })
         .AddTo(this.gameObject);
   }
   
   public override void OnCollisionEnter2D(Collision2D other)
   {
      // do nothing
   }
}