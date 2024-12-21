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

    [System.NonSerialized]
    public bool isGameStart;

    [System.NonSerialized]
    public bool isGameClear;

    [System.NonSerialized]
    public bool isGameOver;

    public AudioSource titleBGM;
    public AudioSource inGameBGM;
    public AudioSource gameOverBGM;
    public AudioSource resultBGM;
    public AudioSource nowBGM;

    public int Remaining_Time
    {
        set { remaining_time = value; } //�l�̑��
        get { return remaining_time; } //�O���ɒl��Ԃ�
    }

    public bool GameOver
    {
        set { isGameOver = value; } //�l�̑��
        get { return isGameOver; } //�O���ɒl��Ԃ�
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

    void Update(){
        HandleTitle();
        HandleGameStart();
        HandleGameClear();
        HandleGameOver();
    }

    public void HandleTitle(){
        if(!isGameStart && !isGameClear && !isGameOver){
            StopMusic();
            StartMusic(titleBGM);
        }
    }

	public void HandleGameStart(){
        if(isGameStart){
            StopMusic();
            StartMusic(inGameBGM);
        }
	}

    public void HandleGameClear(){
        if(isGameClear){
            StopMusic();
            StartSound(resultBGM);
        }
    }

    public void HandleGameOver(){
        if(isGameOver){
            StopMusic();
            StartMusic(gameOverBGM);
        }
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
        nowBGM.loop = true;
        nowBGM.Play();
    }
    
    // SEを再生
    public void StartSound(AudioSource audio){
        nowBGM = audio;
        nowBGM.Play();
    }

    // BGMをストップ
    public void StopMusic(){
        if(nowBGM != null){
            nowBGM.Stop();
        }
    }
}
