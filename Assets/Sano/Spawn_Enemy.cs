using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    // ���X�g�̗v�f�̃C���f�b�N�X�͂��ꂼ��̃I�u�W�F�N�g�ɑΉ�
    // ��Œ�������

    // �G�����𔻒肷��p�x�i�ŏ��j
    [SerializeField] private float enemy_min_time_span;
    // �K�����𔻒肷��p�x�i�Ō�j
    [SerializeField] private float enemy_min_time_span_final;
    // �G�L�����̃C���X�^���X������
    [SerializeField] private List<GameObject> enemyList;
    // �G�L�����̈ړ����x
    [SerializeField] private List<float> enemySpeed;
    // ���ꂼ��̓G�L�����̐����m��������i���v1�𒴂��Ȃ��悤�ɁA1�����̕��͐������Ȃ������j
    [SerializeField] private List<float> enemy_ratio_first;
    // �G�L�����̍ŏI�I�Ȑ����m��
    [SerializeField] private List<float> enemy_ratio_finish;
    // ���ꂼ��̃L�����̃X�|�[���ʒu
    [SerializeField] private List<Vector2> ground_spawn_position;

    [SerializeField] private float speed_range = 0.2f;

    private List<float> enemy_ratio_diff = new List<float>();
    private float enemy_min_time_span_diff = 0.0f;
    private float remaining_time = 60.0f;    // ���ƂłȂ�Ƃ�����
    private float start_time = 0.0f;
    private float prev_time = 0.0f;

    private TimerManager m_timer;
    private GameController gc;


    // Start is called before the first frame update
    void Start()
    {
        // �ŏ��Ɉ�̂����S�u�����𐶐�
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

        m_timer
            .CountDownTime
            .Where(x => x <= 0)
            .Subscribe(_=>this.gameObject.SetActive(false))
            .AddTo(this.gameObject);
    }

    IEnumerator Spawn_Enemies()
    {
        while (true)    // �����d���������炲�߂�Ȃ���
        {
            if (gc.isGameStart)
            {
                break;
            }
            yield return null;
        }

        while (gc.isGameStart && !gc.isGameClear)
        {
            // �����_���œG�𐶐�
            GameObject enemy = null;
            float speed = 1.0f;
            Vector2 spawn_position = Vector2.zero;
            float rand = Random.Range(0.0f, 1.0f);
            float ratio_sum = 0.0f;
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemy_ratio_first[i] += enemy_ratio_diff[i] * (start_time - m_timer.CountDownTime.Value - prev_time);    // �����m�����X�V
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
            // �������Ȃ��ꍇ
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
        if (GameController.instance.isGameClear)
        {
            Debug.Log("clear");
            this.gameObject.SetActive(false);
        }
    }
}
