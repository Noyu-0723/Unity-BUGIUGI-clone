using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 雑魚敵
public class Dragon : Enemy
{
    protected override void Move()
    {
        // 正弦波のように動く

        Vector2 dist = new Vector2(-speed * Time.deltaTime, Mathf.PingPong(Time.time, 0.3f));

    }

    protected override void Spawn()
    {
        // 高い位置にスポーンするようにする
        Vector2 spawnPosition = m_camera.ViewportToWorldPoint(new Vector2(1.0f, 0.8f));
        m_rig.transform.position = spawnPosition;
    }
}
