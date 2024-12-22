using UnityEngine;

public class ResultWidgetController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private CanvasGroup _resultCanvasGroup;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] float _viewDuration = 0.5f;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [SerializeField] private ResultWidget _resultWidget;
    
    private void Start()
    {
        Initialize();
        gameObject.SetActive(false);
    }

	private void Update()
	{
        if (GameController.instance.isGameClear || GameController.instance.isGameOver)
        {
            gameObject.SetActive(true);
            View(GameController.instance.isGameClear);
        }
	}

	private void Initialize()
    {
        _resultCanvasGroup.alpha = 0;
        _resultCanvasGroup.blocksRaycasts = false;
        _resultCanvasGroup.interactable = false;
    }

    /// <summary>
    /// 
    /// </summary>
    public void View(bool isClear)
    {
        _resultWidget.SetView(isClear);
        
        AnimationUtility.FadeInCanvasGroupTween(_resultCanvasGroup,_viewDuration);
        _resultCanvasGroup.blocksRaycasts = true;
        _resultCanvasGroup.interactable = true;
    }
}
