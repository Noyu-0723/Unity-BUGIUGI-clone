using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* メモ
 * ・最初はdisableにしてinstantiate, OnEnableで動き始めるようにする
 * ・Playerとのやりとり
 * 　・isTargeting    // ターゲットするときに呼び出す
 * 　・void PositionChanged()    // プレイヤーとの座標の入れ替えが起こった時に呼び出す
 * */


public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject m_target_mark;
    [SerializeField] protected GameObject m_question_mark;
    protected float question_mark_time = 2.0f;
    protected float attack_time = 1.0f;
    protected float dead_time = 2.0f;

    [SerializeField] private Vector2 m_spawnPosition = new Vector2(10, -1);

    protected Camera m_camera;
    public bool isTargeting = false;

    // とりあえずprivate
    protected int hp = 1;
    protected float speed = 1.0f;
    //private float attack = 1.0f;

    protected Rigidbody2D m_rig;
    protected Animator m_animator;
    protected bool _isDead = false;
    protected bool _isAttacking = false;

    [SerializeField] private bool _tmp_isPositionChanged = false;

    private void OnEnable()
    {
        m_camera = GameObject.Find("Main Camera").GetComponent<Camera>();//Camera.current;
        m_rig = this.GetComponent<Rigidbody2D>();
        m_animator = this.GetComponent<Animator>();
        Spawn();
    }

    // Start is called before the first frame update
    public virtual void Start()
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
        //Vector2 spawnPosition = m_camera.ViewportToWorldPoint(new Vector2(1.0f, 0.5f));
        m_spawnPosition.y = m_rig.position.y;

        //m_rig.position = m_spawnPosition;
    }

    protected virtual void Move()
    {
        // ひとまず座標を直接更新する方式にする
        m_rig.position += new Vector2(-speed * Time.deltaTime, 0);
    }

    // プレイヤーにアタックする
    // プレイヤー側からの被ダメはプレイヤー自身で完結するため、アタックのモーションだけ実行する
    protected void AttackPlayer()
    {
        // 攻撃のアニメーションを実行
        if (!_isDead && !_isAttacking)
        {
            StartCoroutine("Attack_anim");
        }
    }

    // 城に攻撃する
    protected void AttackCastle()
    {
        // 城に衝突
        // ゲームオーバーにする？
    }

    protected void Dead()
    {
        m_rig.constraints = RigidbodyConstraints2D.FreezePosition;
        //  死んだときの動作をここに書く
        if (!_isDead)
        {
            m_animator.SetBool("Die", true);
            m_question_mark.SetActive(false);
            _isDead = true;
            this.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Destroy_enemy()
    {
        Destroy(this.gameObject);
    }

    IEnumerator SetQuestionMark()
    {
        m_question_mark.SetActive(true);
        float tmp_speed = this.speed;
        this.speed = 0.0f;
        m_animator.SetBool("Stand", true);
        yield return new WaitForSeconds(question_mark_time);
        this.speed = tmp_speed;
        m_question_mark.SetActive(false);
        m_animator.SetBool("Stand", false);
    }

    IEnumerator Attack_anim()
    {
        _isAttacking = true;
        m_animator.SetBool("Attack", true);
        float tmp_speed = this.speed;
        this.speed = 0.0f;
        yield return new WaitForSeconds(attack_time);
        m_animator.SetBool("Attack", false);
        this.speed = tmp_speed;
        _isAttacking = false;
    }

    

    // プレイヤーからのダメージを受ける（Playerから呼び出し）
    protected void TakeDamage(int ap)
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
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject opponent = collision.gameObject;    // 衝突相手を取得
        Collider2D other = opponent.GetComponent<Collider2D>();

        if (other.CompareTag("Attack"))
        {
            TakeDamage(1);
        }
        else if (other.CompareTag("Player"))
        {
            AttackPlayer();
        }
        else if (other.CompareTag("Castle"))
        {
            AttackCastle();
        }
    }
}