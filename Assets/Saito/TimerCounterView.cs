using UnityEngine;
using UnityEngine.UI;

public class TimerCounterView : MonoBehaviour
{
   /// <summary>
   /// 
   /// </summary>
   [SerializeField] private Text _text;
   
   /// <summary>
   /// 
   /// </summary>
   /// <param name="time"></param>
   public void SetView(float time)
   {
       _text.text = time.ToString("F2");
   }
}
