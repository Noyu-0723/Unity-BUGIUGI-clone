using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    private int playerHp = 1;
    private int playerAttack = 1;
    private Rigidbody2D rb;
    private Collider2D playerCollider2D;
    private Animator animator;
    public float movementSpeed = 5f; // 移動速度
    public bool canMove = true;
    public float attackMoveLockDuration = 0.8f; // 攻撃モーション中に動けないフレーム
    public float defeatMoveLockDuration = 3.0f; // 死亡モーション中に動けないフレーム
    public bool isGrounded = false; // 地面と接しているのかどうか
    public float replacementCooldown = 10f; // 入れ替えスキルのクールダウン
    public bool isTarget = false;
    public Enemy targetingEnemy = null;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        playerCollider2D = GetComponent<Collider2D>();
        // animator = GetComponent<Animator>();
    }
    void Update(){
        if(canMove){
            HandleMovement();
            HandleEnemyTarget();
            HandleReplacement();
            HandleAttack();
            HandleCheckDeath();
            if (rb.velocity.y < 0){
                isGrounded = false;
            }
        }
    }
    // 移動に関する処理
    private void HandleMovement(){
        if(!canMove) return;
        bool isPressingLeft = Input.GetKey(KeyCode.A);
        bool isPressingRight = Input.GetKey(KeyCode.D);
        if(isPressingRight){
            // animator.SetInteger("Direction", 0);
            // animator.SetBool("isWalking", true);
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }
        else if(isPressingLeft){
            // animator.SetInteger("Direction", 1);
            // animator.SetBool("isWalking", true);
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
        }
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if(!isGrounded && rb.velocity.y < 0){
            // animator.SetBool("isSliding", false);
            // animator.SetBool("isFalling", true);
        }
    }
    // 敵のターゲティング
    private void HandleEnemyTarget(){
        if(Input.GetMouseButtonDown(0)){
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapCircle(mousePosition, 0.1f);
            if(hitCollider != null && hitCollider.CompareTag("Enemy")){
                targetingEnemy = hitCollider.GetComponent<Enemy>();
                if(targetingEnemy != null){
                    isTarget = true;
                    targetingEnemy.isTargeting = true;
                }
            }else{
                isTarget = false;
                targetingEnemy.isTargeting = false;
            }
        }
    }
    // 入れ替えスキルの発動
    private void HandleReplacement(){
        if(Input.GetMouseButtonDown(1) && isTarget){
            targetingEnemy.PositionChanged();
            Vector2 enemyPosition = targetingEnemy.transform.position;
            targetingEnemy.transform.position = this.transform.position;
            this.transform.position = enemyPosition;
        }
    }
    // 通常攻撃
    private void HandleAttack(){
        if(Input.GetKeyDown(KeyCode.Space)){
            // animator.SetTrigger("isAttack");
            StartCoroutine(MoveLock(attackMoveLockDuration));
        }
    }
    // 死亡判定
    private void HandleCheckDeath(){
        if(playerHp <= 0){
            // animator.SetTrigger("isDefeat");
            StartCoroutine(MoveLock(defeatMoveLockDuration));
        }
    }
    // 敵との接触判定
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Enemy")){
            // animator.SetTrigger(isDamage);
            // StartCoroutine(MoveLock(defeatMoveLockDuration));
            // Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            playerHp -= 1; // 仮実装
        }
    }
    // 行動不能状態
    IEnumerator MoveLock(float duration){
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }
}
