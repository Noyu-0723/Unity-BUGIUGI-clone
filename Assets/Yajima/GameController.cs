using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	#region 矢島

	// インスタンス化
	public static GameController instance { get; private set; }

    [SerializeField]
    private int remaining_time;

    private bool isGameStart;
    private bool isGameClear;
    private bool isGameOver;

    public int Remaining_Time
    {
        set { remaining_time = value; } //値の代入
        get { return remaining_time; } //外部に値を返す
    }

	#endregion

	void Awake()
	{
		if(instance == null)
		{
            instance = this;
		}
	}

	void Start()
	{
        isGameStart = false;
        isGameClear = false;
        isGameOver = false;
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

    // ゲーム終了関数
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //このコードでビルドしたゲームを終了することができる
        Application.Quit();
#endif
    }
}
