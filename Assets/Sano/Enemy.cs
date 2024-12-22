using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* ����
 * �E�ŏ���disable�ɂ���instantiate, OnEnable�œ����n�߂�悤�ɂ���
 * �EPlayer�Ƃ̂��Ƃ�
 * �@�EisTargeting    // �^�[�Q�b�g����Ƃ��ɌĂяo��
 * �@�Evoid PositionChanged()    // �v���C���[�Ƃ̍��W�̓���ւ����N���������ɌĂяo��
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

    // �Ƃ肠����private
    protected int hp = 1;
    protected float speed = 1.0f;
    //private float attack = 1.0f;

    protected Rigidbody2D m_rig;
    protected Animator m_animator;
    protected bool _isDead = false;
    protected bool _isAttacking = false;

    [SerializeField] private bool _tmp_isPositionChanged = false;
    public EnemySensor sensor;
    public GameObject attackJDG;
    public float attackBeforeDuration;

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

        // �ȉ��f�o�b�O�p
        if (_tmp_isPositionChanged)
        {
            PositionChanged();
            _tmp_isPositionChanged = false;
        }

        if(sensor.isAttackOther){
            AttackPlayer();
        }
    }

    // �I�u�W�F�N�g�𐶐������Ƃ��̏���
    // OnEnable()����Ăяo��
    protected virtual void Spawn()
    {
        // �X�|�[���̏ꏊ���w��
        //Vector2 spawnPosition = m_camera.ViewportToWorldPoint(new Vector2(1.0f, 0.5f));
        m_spawnPosition.y = m_rig.position.y;

        //m_rig.position = m_spawnPosition;
    }

    protected virtual void Move()
    {
        // �ЂƂ܂����W�𒼐ڍX�V��������ɂ���
        m_rig.position += new Vector2(-speed * Time.deltaTime, 0);
    }

    // �v���C���[�ɃA�^�b�N����
    // �v���C���[������̔�_���̓v���C���[���g�Ŋ������邽�߁A�A�^�b�N�̃��[�V�����������s����
    protected void AttackPlayer()
    {
        // �U���̃A�j���[�V���������s
        if (!_isDead && !_isAttacking)
        {
            StartCoroutine("Attack_anim");
            StartCoroutine(AttackRange(attackBeforeDuration));
        }
    }
    IEnumerator AttackRange(float before){
        yield return new WaitForSeconds(before);
        attackJDG.SetActive(true);
        yield return new WaitForSeconds(0.001f);
        attackJDG.SetActive(false);
    }

    // ��ɍU������
    protected void AttackCastle()
    {
        // ��ɏՓ�
        // �Q�[���I�[�o�[�ɂ���H
    }

    protected void Dead()
    {
        m_rig.constraints = RigidbodyConstraints2D.FreezePosition;
        //  ���񂾂Ƃ��̓���������ɏ���
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

    

    // �v���C���[����̃_���[�W���󂯂�iPlayer����Ăяo���j
    protected void TakeDamage(int ap)
    {
        this.hp -= ap;
        if (this.hp <= 0)
        {
            this.hp = 0;
            Dead();
        }
    }

    // �^�[�Q�b�g���ꂽ���ɌĂ΂��
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
        // ���W������ւ�������̂͂Ăȃ}�[�N
        StartCoroutine("SetQuestionMark");
    }



    // �Փˑ���̔����Tag��
    // Player or Castle
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject opponent = collision.gameObject;    // �Փˑ�����擾
        Collider2D other = opponent.GetComponent<Collider2D>();

        if (other.CompareTag("Attack"))
        {
            TakeDamage(1);
        }
    }
}