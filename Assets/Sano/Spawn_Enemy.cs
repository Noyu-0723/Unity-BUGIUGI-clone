using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy_goblin;
    [SerializeField] private GameObject enemy_dragon;
    private float enemy_time_span = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawn_Enemies");
    }

    IEnumerator Spawn_Enemies()
    {
        while (true)
        {
            Vector2 position = new Vector2(10.0f, -2.0f);
            Transform t = this.transform;
            t.position = position;

            Instantiate(enemy_goblin, t);
            yield return new WaitForSeconds(enemy_time_span);
            GameObject obj = Instantiate(enemy_dragon);
            obj.SetActive(false);
            yield return new WaitForSeconds(enemy_time_span);
            obj.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // do nothing
    }
}
