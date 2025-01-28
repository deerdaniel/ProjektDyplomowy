using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other) 
    { 
    
        if (other.gameObject.tag == "EnemyCollider")
        {
            FindAnyObjectByType<AudioManager>().Play("ZombieDeath");
            Destroy(other.gameObject);
        }
    }
}
