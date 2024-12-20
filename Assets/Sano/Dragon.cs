using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// éGãõìG
public class Dragon : Enemy
{
    protected override void Move()
    {
        // ê≥å∑îgÇÃÇÊÇ§Ç…ìÆÇ≠

        //Mathf.PingPong(Time.time, 0.3f);


    }

    protected override void Spawn()
    {
        Vector2 spawnPosition = m_camera.ViewportToWorldPoint(new Vector2(1.0f, 0.8f));
        m_rig.transform.position = spawnPosition;
    }
}
