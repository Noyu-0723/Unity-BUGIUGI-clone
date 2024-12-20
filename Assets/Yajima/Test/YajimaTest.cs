using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YajimaTest : MonoBehaviour
{
    int num;

    // Start is called before the first frame update
    void Start()
    {
        num = GameController.instance.Remaining_Time;
        Debug.Log(num);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
