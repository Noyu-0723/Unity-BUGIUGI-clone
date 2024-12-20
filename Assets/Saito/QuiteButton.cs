using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class QuiteButton : MonoBehaviour
{
    [SerializeField] private Button _quiteButton;

    private void Start()
    {
        _quiteButton
            .OnClickAsObservable()
            .Subscribe(_=>Application.Quit())
            .AddTo(this.gameObject);
    }
}
