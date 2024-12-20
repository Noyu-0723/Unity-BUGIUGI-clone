using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // とりあえずprivate
    private int hp = 1;
    private float speed = 1.0f;
    private float attack = 1.0f;

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

    private void Move()
    {
        // ひとまず座標を直接更新する方式にする
        m_rig.position += new Vector2(-speed * Time.deltaTime, 0);
    }

    // プレイヤーからのダメージを受ける
    public void TakeDamage(int ap)
    {

    
    }
}
