using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	#region �

	// �C���X�^���X��
	public static GameController instance { get; private set; }

    [SerializeField]
    private int remaining_time;

    private bool isGameStart;
    private bool isGameClear;
    private bool isGameOver;
    public AudioSource titleBGM;
    public AudioSource inGameBGM;
    public AudioSource resultBGM;
    public AudioSource nowBGM;

    public int Remaining_Time
    {
        set { remaining_time = value; } //�l�̑��
        get { return remaining_time; } //�O���ɒl��Ԃ�
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
        StartMusic(titleBGM);
        isGameStart = false;
        isGameClear = false;
        isGameOver = false;
    }

	public void GameStart()
	{
        StopMusic();
        StartMusic(inGameBGM);
	}

    public void SpawnEnemy()
	{

	}

    public void GameClear()
    {
        StopMusic();
        StartMusic(resultBGM);
    }

    public void GameOver()
    {

    }

    // �Q�[���I���֐�
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //���̃R�[�h�Ńr���h�����Q�[�����I�����邱�Ƃ��ł���
        Application.Quit();
#endif
    }

    // BGMを再生
    public void StartMusic(AudioSource audio){
        nowBGM = audio;
        nowBGM.volume = 0.3f;
        nowBGM.loop = true;
        nowBGM.Play();
    }

    // BGMをストップ
    public void StopMusic(){
        if(nowBGM != null){
            nowBGM.Stop();
        }
    }
}
