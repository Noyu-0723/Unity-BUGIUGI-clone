using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    // リストの要素のインデックスはそれぞれのオブジェクトに対応
    // 後で直したい

    // 敵生成を判定する頻度（最初）
    [SerializeField] private float enemy_min_time_span;
    // 適性性を判定する頻度（最後）
    [SerializeField] private float enemy_min_time_span_final;
    // 敵キャラのインスタンスを入れる
    [SerializeField] private List<GameObject> enemyList;
    // 敵キャラの移動速度
    [SerializeField] private List<float> enemySpeed;
    // それぞれの敵キャラの生成確率を入れる（合計1を超えないように、1未満の分は生成しない割合）
    [SerializeField] private List<float> enemy_ratio_first;
    // 敵キャラの最終的な生成確率
    [SerializeField] private List<float> enemy_ratio_finish;
    // それぞれのキャラのスポーン位置
    [SerializeField] private List<Vector2> ground_spawn_position;

    [SerializeField] private float speed_range = 0.2f;

    private List<float> enemy_ratio_diff = new List<float>();
    private float enemy_min_time_span_diff = 0.0f;
    private float remaining_time = 60.0f;    // あとでなんとかする
    private float start_time = 0.0f;
    private float prev_time = 0.0f;

    private TimerManager m_timer;
    private GameController gc;


    // Start is called before the first frame update
    void Start()
    {
        // 最初に一体だけゴブリンを生成
        //GameObject obj = Instantiate(enemyList[0], ground_spawn_position[0], Quaternion.identity);
        //obj.GetComponent<Enemy>().speed = enemySpeed[0];
        //obj.SetActive(true);

        m_timer = GameObject.Find("TimerManager").GetComponent<TimerManager>();
        remaining_time = m_timer.CountDownTime.Value;
        start_time = remaining_time;

        for (int i = 0; i <  enemyList.Count; i++) 
        {
            float diff = (enemy_ratio_finish[i] - enemy_ratio_first[i]) / remaining_time;
            enemy_ratio_diff.Add(diff);
        }
        enemy_min_time_span_diff = (enemy_min_time_span_final - enemy_min_time_span) / remaining_time;

        gc = GameObject.Find("GameController").GetComponent<GameController>();

        StartCoroutine("Spawn_Enemies");
    }

    IEnumerator Spawn_Enemies()
    {
        while (true)    // 処理重たかったらごめんなさい
        {
            if (gc.isGameStart)
            {
                break;
            }
            yield return null;
        }

        while (gc.isGameStart && !gc.isGameClear)
        {
            // ランダムで敵を生成
            GameObject enemy = null;
            float speed = 1.0f;
            Vector2 spawn_position = Vector2.zero;
            float rand = Random.Range(0.0f, 1.0f);
            float ratio_sum = 0.0f;
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemy_ratio_first[i] += enemy_ratio_diff[i] * (start_time - m_timer.CountDownTime.Value - prev_time);    // 生成確率を更新
                if (enemy_ratio_first[i] < 0)
                {
                    enemy_ratio_first[i] = 0;
                }
            }
            enemy_min_time_span += enemy_min_time_span_diff * (start_time - m_timer.CountDownTime.Value - prev_time);

            prev_time = start_time - m_timer.CountDownTime.Value;

            for (int i = 0; i < enemyList.Count; i++)
            {
                ratio_sum += enemy_ratio_first[i];
                if (rand < ratio_sum)
                {
                    enemy = enemyList[i];
                    spawn_position = ground_spawn_position[i];
                    speed = enemySpeed[i] + Random.Range(-speed_range, speed_range);
                    break;
                }
            }
            // 生成しない場合
            if (enemy != null)
            {
                GameObject obj = Instantiate(enemy, spawn_position, Quaternion.identity, this.transform);
                obj.SetActive(true);
                obj.GetComponent<Enemy>().speed = speed;
            }

            yield return new WaitForSeconds(enemy_min_time_span);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // do nothing
        if (gc.isGameClear)
        {
            this.enabled = false;
        }
    }
}
