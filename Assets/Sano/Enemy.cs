using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    // とりあえずprivate
    private int hp = 1;
    private float speed = 1.0f;
    private float attack = 1.0f;

    // スポーンの場所
    protected Vector2 spawnPosition;

    // Trueの間ターゲットされているマークを表示するようにする
    public bool isTargeting = false;

    private Rigidbody2D m_rig;


    // Start is called before the first frame update
    void Start()
    {
        m_rig = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void SetSpawnPosition()
    {
        //
    }

    public void Spawn()
    {
        // オブジェクトを生成したときの処理
        // instantiate自体はmanagerから呼び出す？
    }

    private void Move()
    {
        // ひとまず座標を直接更新する方式にする
        m_rig.position += new Vector2(-speed * Time.deltaTime, 0);
    }

    // プレイヤーにアタックする
    // プレイヤー側からの被ダメはプレイヤー自身で完結するため、アタックのモーションだけ実行する
    private void AttackPlayer()
    {
        
    }

    // 城に攻撃する
    private void AttackCastle()
    {
        // 城に衝突
        // ゲームオーバー
    }

    // プレイヤーからのダメージを受ける
    public void TakeDamage(int ap)
    {
        // 何かする
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突時の動作

        GameObject opponent = collision.gameObject;    // 衝突相手を取得
        if (true)    // あとで衝突相手がプレイヤーかを判定する条件を書く
        {
            AttackPlayer();
        }
        else if (true)    // あとで衝突相手が城かを判定する条件を書く
        {
            AttackCastle();
        }
    }
}
