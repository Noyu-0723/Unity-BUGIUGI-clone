using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // インスタンス化
    public static GameController instance { get; private set; }

    [SerializeField]
    private int remaining_time;

    public int Remaining_Time
    {
        set { remaining_time = value; } //値の代入
        get { return remaining_time; } //外部に値を返す
    }

	void Awake()
	{
		if(instance == null)
		{
            instance = this;
		}
	}

    public void GameStart()
	{

	}

    public void SpawnEnemy()
	{

	}

    public void GameClear()
    {

    }

    public void GameOver()
    {

    }
}
