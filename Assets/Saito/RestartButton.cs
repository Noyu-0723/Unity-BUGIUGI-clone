using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    private void Start()
    {
        _restartButton
            .OnClickAsObservable()
            .Subscribe(_=>SceneManager.LoadScene(SceneManager.GetActiveScene().name))
            .AddTo(this.gameObject);
    }
}
