using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* メモ
 * ・最初はdisableにしてinstantiate, OnEnableで動き始めるようにする
 * ・Playerとのやりとり
 * 　・isTargeting    // ターゲットするときに呼び出す
 * 　・void TakeDamage()    // プレイヤーが攻撃アクションをした時に呼び出す
 * 　・void PositionChanged()    // プレイヤーとの座標の入れ替えが起こった時に呼び出す
 * */


public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject m_target_mark;
    [SerializeField] protected GameObject m_question_mark;
    private float question_mark_time = 2.0f;

    protected Camera m_camera;
    public bool isTargeting = false;

    // とりあえずprivate
    protected int hp = 1;
    protected float speed = 1.0f;
    //private float attack = 1.0f;

    protected Rigidbody2D m_rig;

    [SerializeField] private bool _tmp_isPositionChanged = false;


    private void OnEnable()
    {
        m_camera = GameObject.Find("Main Camera").GetComponent<Camera>();//Camera.current;
        m_rig = this.GetComponent<Rigidbody2D>();
        Spawn();
    }

    // Start is called before the first frame update
    void Start()
    {
        // do nothing
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Targeting();

        // 以下デバッグ用
        if (_tmp_isPositionChanged)
        {
            PositionChanged();
            _tmp_isPositionChanged = false;
        }
    }

    // オブジェクトを生成したときの処理
    // OnEnable()から呼び出す
    protected virtual void Spawn()
    {
        // スポーンの場所を指定
        Vector2 spawnPosition = m_camera.ViewportToWorldPoint(new Vector2(1.0f, 0.5f));
        m_rig.transform.position = spawnPosition;
    }

    protected virtual void Move()
    {
        // ひとまず座標を直接更新する方式にする
        m_rig.position += new Vector2(-speed * Time.deltaTime, 0);
    }

    // プレイヤーにアタックする
    // プレイヤー側からの被ダメはプレイヤー自身で完結するため、アタックのモーションだけ実行する
    private void AttackPlayer()
    {
        // 攻撃のアニメーションを実行
        Dead();    // 一旦deadにする
    }

    // 城に攻撃する
    private void AttackCastle()
    {
        // 城に衝突
        // ゲームオーバーにする？
    }

    private void Dead()
    {
        //  死んだときの動作をここに書く

        // とりあえずGameObjectを削除
        Destroy(this.gameObject);
    }

    IEnumerator SetQuestionMark()
    {
        Debug.Log("test");
        m_question_mark.SetActive(true);
        float tmp_speed = this.speed;
        this.speed = 0.0f;
        yield return new WaitForSeconds(question_mark_time);
        this.speed = tmp_speed;
        m_question_mark.SetActive(false);
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

    // ターゲットされた時に呼ばれる
    public void Targeting()
    {
        if (isTargeting && !m_target_mark.activeSelf)
        {
            m_target_mark.SetActive(true);
        }
        else if (!isTargeting && m_target_mark.activeSelf)
        {
            m_target_mark.SetActive(false);
        }
    }

    public virtual void PositionChanged()
    {
        // 座標が入れ替わった時のはてなマーク
        StartCoroutine("SetQuestionMark");
    }



    // 衝突相手の判定はTagで
    // Player or Castle
    public void OnCollisionEnter2D(Collision2D collision)
    {

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

