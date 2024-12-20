using UniRx;
using UnityEngine;

public class LockOnMarkController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Canvas _canvas;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private RectTransform _canvasRectTransform;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private CanvasGroup _lockOnMarkCanvasGroup;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private RectTransform _lockOnMarkRectTransform;

    private void Start()
    {
        Observable
            .EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Where(_ =>
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hit = Physics2D.Raycast(ray.origin, ray.direction);

                return hit.collider != null && hit.collider.CompareTag("Enemy");
            })
            .Subscribe(_ =>
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _canvasRectTransform,
                    Input.mousePosition,
                    _canvas.worldCamera, 
                    out var mousePositionToLocalPoint);
                _lockOnMarkRectTransform.anchoredPosition = mousePositionToLocalPoint;
                _lockOnMarkCanvasGroup.alpha = 1;
            })
            .AddTo(this.gameObject);
    }
}