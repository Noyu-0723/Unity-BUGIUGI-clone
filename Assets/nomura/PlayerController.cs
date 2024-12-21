using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    private int playerMaxHp = 1;
    private int playerHp;
    private int playerAttack; 
    private Rigidbody2D rb;
    private Collider2D playerCollider2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public Vector2 respawnPosition; // リスポーン地点
    public Enemy targetingEnemy = null; // 現在ターゲティングしている敵
    public Collider2D normalAttackCollider2D; // 通常攻撃のコリジョン
    public Collider2D airAttackCollider2D; // 落下攻撃のコリジョン
    public AudioSource attackSE; // 攻撃時のSE
    public AudioSource defeatSE; // 敗北時のSE
    public AudioSource replaceSE; // 入れ替わり時のSE
    public AudioSource explosionSE; // 落下攻撃時のSE
    public bool canMove = true; // 移動可能なのかどうか
    public bool isTarget = false; // 敵をターゲティングしているのかどうか
    public bool isGrounded = false; // 地面と接しているのかどうか
    public float movementSpeed = 5f; // 移動速度
    public float attackMoveLockDuration = 0.8f; // 攻撃モーション中に動けないフレーム
    public float defeatMoveLockDuration = 3.0f; // 死亡モーション中に動けないフレーム
    public float replacementCooldown = 5.0f; // 入れ替えスキルのクールダウン
    void Start(){
        playerHp = playerMaxHp;
        playerAttack = 1;
        rb = GetComponent<Rigidbody2D>();
        playerCollider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update(){
        if(canMove && isGrounded){
            HandleMovement();
            HandleEnemyTarget();
            HandleReplacement();
            HandleAttack();
            HandleCheckDeath();
        }
    }
    // 移動に関する処理
    private void HandleMovement(){
        if(!canMove) return;
        Vector3 myPosition = this.transform.position;
        bool isPressingLeft = Input.GetKey(KeyCode.A);
        bool isPressingRight = Input.GetKey(KeyCode.D);
        if(isPressingRight){
            spriteRenderer.flipX = false;
            animator.SetBool("isWalking", true);
            myPosition.x += movementSpeed * Time.deltaTime;
        }
        else if(isPressingLeft){
            spriteRenderer.flipX = true;
            animator.SetBool("isWalking", true);
            myPosition.x -= movementSpeed * Time.deltaTime;
        }
        if(!isGrounded && rb.velocity.y < 0){
            animator.SetBool("isWalking", false);
            // animator.SetBool("isFalling", true);
        }else{
            // animator.SetBool("isFalling", false);
        }
        this.transform.position = myPosition;
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
            replaceSE.Play();
            targetingEnemy.PositionChanged();
            Vector2 enemyPosition = targetingEnemy.transform.position;
            targetingEnemy.transform.position = this.transform.position;
            this.transform.position = enemyPosition;
        }
    }
    // 通常攻撃
    private void HandleAttack(){
        if(Input.GetKeyDown(KeyCode.Space)){
            // animator.play("Attack");
            StartCoroutine(MoveLock(attackMoveLockDuration));
            StartCoroutine(NormalAttack());
        }
    }
    // 死亡判定
    private void HandleCheckDeath(){
        if(playerHp <= 0){
            // animator.play("Defeat");
            defeatSE.Play();
            StartCoroutine(MoveLock(defeatMoveLockDuration));
            StartCoroutine(Respawn(defeatMoveLockDuration));
        }
    }
    // 接触判定
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Enemy")){
            if(isGrounded){
                // animator.play(TakeDamage);
                // Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                playerHp -= 1; // 仮実装
            }else{
                StartCoroutine(AirAttack());
            }
            
        }
    }
    // 接地判定
    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Floor")){
            isGrounded = true;
        }
    }
    // 地面から離れたことを感知
    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Floor")){
            isGrounded = false;
        }
    }
    // 行動不能状態
    IEnumerator MoveLock(float duration){
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }
    // リスポーン
    IEnumerator Respawn(float duration){
        yield return new WaitForSeconds(duration);
        // animator.play("Respawn");
        playerHp = playerMaxHp;
        this.transform.position = respawnPosition;
    }
    // 通常攻撃の判定処理
    IEnumerator NormalAttack(){
        yield return new WaitForSeconds(0.1f);
        attackSE.Play();
        normalAttackCollider2D.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        normalAttackCollider2D.gameObject.SetActive(false);
    }
    // 落下攻撃の判定処理
    IEnumerator AirAttack(){
        yield return new WaitForSeconds(0.1f);
        explosionSE.Play();
        airAttackCollider2D.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        airAttackCollider2D.gameObject.SetActive(false);
    }
}
