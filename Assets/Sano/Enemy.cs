using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* メモ
 * ・最初はdisableにしてinstantiate, OnEnableで動き始めるようにする
 * 
 * */


public class Enemy : MonoBehaviour
{
    [SerializeField] private Camera m_camera;

    // とりあえずprivate
    private int hp = 1;
    private float speed = 1.0f;
    private float attack = 1.0f;


    // Trueの間ターゲットされているマークを表示するようにする
    public bool isTargeting = false;

    private bool isAttacked = false;    // trueの間敵と衝突してもAttackPlayerを呼ばない
    private Rigidbody2D m_rig;


    private void OnEnable()
    {
        Spawn();
    }

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

    // オブジェクトを生成したときの処理
    // OnEnable()から呼び出す
    protected virtual void Spawn()
    {
        // スポーンの場所を指定
        Vector2 spawnPosition = m_camera.ViewportToWorldPoint(new Vector2(1.0f, 0.5f));
        m_rig.transform.position = spawnPosition;
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
        // 攻撃のアニメーション
    }

    // 城に攻撃する
    private void AttackCastle()
    {
        // 城に衝突
        // ゲームオーバーにする？
    }

    // プレイヤーからのダメージを受ける（Playerから呼び出し）
    public void TakeDamage(int ap)
    {
        this.hp -= ap;
        if (this.hp <= 0)
        {
            this.hp = 0;
            Dead();
        }
    }

    public void Dead()
    {
        //  死んだときの動作をここに書く

        // とりあえずGameObjectを削除
        Destroy(this.gameObject);
    }

    // 衝突相手の判定はTagで
    // Player or Castle
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突時の動作

        GameObject opponent = collision.gameObject;    // 衝突相手を取得
        Collider2D other = opponent.GetComponent<Collider2D>();

        if (other.CompareTag("Player"))
        {
            AttackPlayer();
        }
        else if (other.CompareTag("Castle"))
        {
            AttackCastle();
        }
    }
}
