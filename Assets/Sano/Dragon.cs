using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 雑魚敵
public class Dragon : Enemy
{
    [SerializeField] private float m_move_wave_amp;
    [SerializeField] private float m_move_wave_freq;    // 注：単位は適当（Hzではない）

    private float m_spawn_y = 0.0f;
    private float m_current_y = 0.0f;
    private float m_current_move_wave_amp = 1.0f;

    private bool m_isPositionChanged = false;

    protected override void Move()
    {
        base.Move();

        if (this.speed == 0.0f)
        {
            return;
        }

        // 落下を検出（なんとかしたい）
        if (this.m_rig.position.y < m_spawn_y - m_move_wave_amp && !m_isPositionChanged)
        {
            m_current_move_wave_amp = 0.0f;
            m_current_y = this.m_rig.position.y;
            m_isPositionChanged = true;
        }

        // 下に落ちた時
        if (m_current_y < m_spawn_y)
        {
            m_current_y += speed * Time.deltaTime / 1.4142f;
            m_current_move_wave_amp = (m_current_move_wave_amp >= 1.0f) ? 1.0f : m_current_move_wave_amp + 1.0f * Time.deltaTime;    // 適当
        }
        else
        {
            m_current_y = m_spawn_y;
            m_current_move_wave_amp = 1.0f;
            m_isPositionChanged = false;
        }

        float y = m_current_y + m_current_move_wave_amp * m_move_wave_amp * Mathf.Sin(Time.time * m_move_wave_freq);

        // 正弦波のように動く
        Vector2 pos = new Vector2(m_rig.position.x, y);
        m_rig.position = pos;
    }

    protected override void Spawn()
    {
        // 高い位置にスポーンするようにする
        Vector2 spawnPosition = m_camera.ViewportToWorldPoint(new Vector2(1.0f, 0.8f));
        m_rig.transform.position = spawnPosition;
        m_spawn_y = m_current_y= spawnPosition.y;
    }

}
