using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    [Header("スポナー")]
    [SerializeField]
    private GameObject[] spawner;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        anim.enabled = false;

        // 子オブジェクトの数を取得
        int childCount = gameObject.transform.childCount;

        // 子オブジェクトを順に取得する
        for (int i = 0; i < childCount; i++)
        {
            Transform childTransform = gameObject.transform.GetChild(i);
            spawner[i] = childTransform.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnExplosion(int id)
    {
        //if (GameController.instance.GameOver)
        {
            anim.enabled = true;

            switch (id)
            {
                case 1: Instantiate(explosion, spawner[0].transform.position, Quaternion.identity); break;
                case 2: Instantiate(explosion, spawner[1].transform.position, Quaternion.identity); break;
                case 3: Instantiate(explosion, spawner[2].transform.position, Quaternion.identity); break;
                case 4: Instantiate(explosion, spawner[3].transform.position, Quaternion.identity); break;
                case 5: Instantiate(explosion, spawner[4].transform.position, Quaternion.identity); break;
                case 6: Instantiate(explosion, spawner[5].transform.position, Quaternion.identity); break;
                default: break;
            }
        }
    }
}
