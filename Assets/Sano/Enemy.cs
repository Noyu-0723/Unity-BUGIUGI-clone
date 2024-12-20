using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int hp = 1;
    public float speed = 1.0f;

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
        // ‚Ğ‚Æ‚Ü‚¸À•W‚ğ’¼ÚXV‚·‚é•û®‚É‚·‚é
        m_rig.position += new Vector2(-speed * Time.deltaTime, 0);
    }
}
