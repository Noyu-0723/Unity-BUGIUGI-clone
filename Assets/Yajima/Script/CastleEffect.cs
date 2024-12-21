using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnExplosion(int id)
    {
        switch (id)
        {
            case 1: Instantiate(explosion, new Vector3(-4.5f, -0.1f, 0.0f), Quaternion.identity); break;
            case 2: Instantiate(explosion, new Vector3(-2.5f, -1.1f, 0.0f), Quaternion.identity); break;
            case 3: Instantiate(explosion, new Vector3(-4.0f, -3.0f, 0.0f), Quaternion.identity); break;
            case 4: Instantiate(explosion, new Vector3(-2.0f, 0.0f, 0.0f), Quaternion.identity); break;
            case 5: Instantiate(explosion, new Vector3(-3.0f, -2.0f, 0.0f), Quaternion.identity); break;
            case 6: Instantiate(explosion, new Vector3(-2.0f, -3.0f, 0.0f), Quaternion.identity); break;
            default: break;
        }
    }
}
