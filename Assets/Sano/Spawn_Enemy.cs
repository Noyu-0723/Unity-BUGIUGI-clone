using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    // リストの要素のインデックスはそれぞれのオブジェクトに対応
    // 後で直したい

    [SerializeField] private float enemy_min_time_span;
    // 敵キャラのインスタンスを入れる
    [SerializeField] private List<GameObject> enemyList;
    // それぞれの敵キャラの生成確率を入れる（合計1を超えないように、1未満の分は生成しない割合）
    [SerializeField] private List<float> enemy_ratio;
    // それぞれのキャラのスポーン位置
    [SerializeField] private List<Vector2> ground_spawn_position;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawn_Enemies");
    }

    IEnumerator Spawn_Enemies()
    {
        while (true)
        {
            // ランダムで敵を生成
            GameObject enemy = null;
            Vector2 spawn_position = Vector2.zero;
            float rand = Random.Range(0.0f, 1.0f);
            float ratio_sum = 0.0f;
            for (int i = 0; i < enemyList.Count; i++)
            {
                ratio_sum += enemy_ratio[i];
                if (rand < ratio_sum)
                {
                    enemy = enemyList[i];
                    spawn_position = ground_spawn_position[i];
                    break;
                }
            }
            // 生成しない場合
            if (enemy != null)
            {
                Instantiate(enemy, spawn_position, Quaternion.identity);
            }

            yield return new WaitForSeconds(enemy_min_time_span);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // do nothing
    }
}
