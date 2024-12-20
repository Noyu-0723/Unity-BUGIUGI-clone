using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemy_goblin;
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
            Instantiate(enemy_goblin);
            yield return new WaitForSeconds(enemy_time_span);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // do nothing
    }
}
