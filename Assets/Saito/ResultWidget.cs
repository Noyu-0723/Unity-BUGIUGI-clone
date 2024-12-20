using UnityEngine;

public class ResultWidget : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private GameObject _gameClearText;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private GameObject _gameOverText;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isClear"></param>
    public void SetView(bool isClear)
    {
        if (isClear)
        {
            _gameClearText.SetActive(true);
            _gameOverText.SetActive(false);
        }
        else
        {
            _gameClearText.SetActive(false);
            _gameOverText.SetActive(true);
        }
    }
}
