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
    public Collider2D normalAttackRightCollider2D; // 通常攻撃のコリジョン
    public Collider2D normalAttackLeftCollider2D; // 通常攻撃のコリジョン
    public Collider2D airAttackCollider2D; // 落下攻撃のコリジョン
    public AudioSource attackSE; // 攻撃時のSE
    public AudioSource defeatSE; // 敗北時のSE
    public AudioSource replaceSE; // 入れ替わり時のSE
    public AudioSource explosionSE; // 落下攻撃時のSE
    public bool canMove = true; // 移動可能なのかどうか
    public bool isReplacementable = true; // 入れ替え可能なのかどうか
    public bool isTarget = false; // 敵をターゲティングしているのかどうか
    public bool isGrounded = true; // 地面と接しているのかどうか
    public float movementSpeed; // 移動速度
    public float attackMoveLockBeforeDuration; // 攻撃の前隙
    public float attackMoveLockAfterDuration; // 攻撃の後隙
    public float airAttackMoveLockBeforeDuration; // 落下攻撃の前隙
    public float defeatMoveLockDuration; // 死亡モーション中に動けないフレーム
    public float replacementCooldown; // 入れ替えスキルのクールダウン
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
        }else if(!isGrounded){
            HandleAirMovement();
        }
    }
    // 移動に関する処理
    private void HandleMovement(){
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
        if(isGrounded && !isPressingRight && !isPressingLeft){
            animator.SetBool("isWalking", false);
        }
        this.transform.position = myPosition;
    }
    private void HandleAirMovement(){
        animator.SetBool("isWalking", false);
        animator.Play("SpecialAttack");
        StartCoroutine(AirAttack(airAttackMoveLockBeforeDuration));
    }
    // 敵のターゲティング
    private void HandleEnemyTarget(){
        if(Input.GetMouseButtonDown(0)){
            if(targetingEnemy != null) targetingEnemy.isTargeting = false; // 敵のターゲットをリセット
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapCircle(mousePosition, 0.1f);
            if(hitCollider != null && hitCollider.CompareTag("Enemy")){
                targetingEnemy = hitCollider.GetComponent<Enemy>();
                isTarget = true;
                targetingEnemy.isTargeting = true;
            }else{
                isTarget = false;
            }
        }
    }
    // 入れ替えスキルの発動
    private void HandleReplacement(){
        if(!isReplacementable) return;
        if(Input.GetMouseButtonDown(1) && isTarget){
            replaceSE.Play();
            targetingEnemy.PositionChanged();
            Vector2 enemyPosition = targetingEnemy.transform.position;
            targetingEnemy.transform.position = this.transform.position;
            this.transform.position = enemyPosition;
            StartCoroutine(SkillLock(replacementCooldown));
        }
    }
    // 通常攻撃
    private void HandleAttack(){
        if(Input.GetKeyDown(KeyCode.Space)){
            animator.Play("Attack");
            StartCoroutine(MoveLock(attackMoveLockBeforeDuration + attackMoveLockAfterDuration));
            StartCoroutine(NormalAttack(attackMoveLockBeforeDuration, attackMoveLockAfterDuration));
        }
    }
    // 死亡判定
    private void HandleCheckDeath(){
        if(playerHp <= 0){
            animator.Play("Die");
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
            }
        }
    }
    // 接地判定
    private void OnCollisionStay2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Floor")){
            isGrounded = true;
        }
    }
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
    // 入れ替え不能状態
    IEnumerator SkillLock(float duration){
        isReplacementable = false;
        yield return new WaitForSeconds(duration);
        isReplacementable = true;
    }
    // リスポーン
    IEnumerator Respawn(float duration){
        yield return new WaitForSeconds(duration);
        playerHp = playerMaxHp;
        this.transform.position = respawnPosition;
        animator.Play("Stand");
    }
    // 通常攻撃の判定処理
    IEnumerator NormalAttack(float before, float after){
        yield return new WaitForSeconds(before);
        attackSE.Play();
        if(!spriteRenderer.flipX){
            normalAttackRightCollider2D.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            normalAttackRightCollider2D.gameObject.SetActive(false);
        }else if(spriteRenderer.flipX){
            normalAttackLeftCollider2D.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            normalAttackLeftCollider2D.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(after);
    }
    // 落下攻撃の判定処理
    IEnumerator AirAttack(float duration){
        yield return new WaitForSeconds(duration);
        Vector3 newPosition = this.transform.position;
        newPosition.y = respawnPosition.y;
        this.transform.position = newPosition;
        explosionSE.Play();
        airAttackCollider2D.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        airAttackCollider2D.gameObject.SetActive(false);
    }
}
