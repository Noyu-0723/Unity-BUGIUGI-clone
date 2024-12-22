using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    public bool isAttackOther = false;
    private void OnTriggerStay2D(Collider2D other){
        if(other.CompareTag("Player") || other.CompareTag("Castle")){
            isAttackOther = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player") || other.CompareTag("Castle")){
            isAttackOther = false;
        }
    }
}
