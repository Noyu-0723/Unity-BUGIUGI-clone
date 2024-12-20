using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    private int hp;
    private int attack;
    private Rigidbody2D rb;
    private Collider2D playerCollider2D;
    private Animator animator;
    public float movementSpeed = 5f; // 移動速度
    public bool canMove = true;
    public float attackMoveLockDuration = 0.8f; // 攻撃モーション中に動けないフレーム
    public bool isGrounded = false; // 地面と接しているのかどうか
    public float replacementCooldown = 10f; // 入れ替えスキルのクールダウン
    public bool isTarget = false;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        playerCollider2D = GetComponent<Collider2D>();
        // animator = GetComponent<Animator>();
    }
    void Update(){
        if(canMove){
            HandleMovement();
            // HandleAttacking();
            // HandleSpecialAbility1();
            // HandleTakingDamage();
            // HandleTemporaryDeath();
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

    private void HandleEnemyTarget(){
        if(Input.GetMouseButtonDown(0)){
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
            if(hitCollider != null && hitCollider.CompareTag("Enemy")){
                Enemy enemy = hitCollider.GetComponent<Enemy>();
                if(enemy != null){
                    isTarget = true;
                    enemy.isTargeting = true;
                }
            }
            else{
                isTarget = false;
            }
        }
    }
}