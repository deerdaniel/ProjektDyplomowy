using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandCollider : MonoBehaviour
{
    public int damage = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            FindAnyObjectByType<AudioManager>().Play("ZombieHit");
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
