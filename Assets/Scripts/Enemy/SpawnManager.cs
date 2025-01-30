using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Enemy;
    public Transform[] SpawnPoints;
    void Start()
    {
        foreach (Transform t in SpawnPoints)
        {
            SpawnEnemy(t);
        }
    }
    public void SpawnEnemy(Transform tran)
    {
        Instantiate(Enemy, tran.position, Quaternion.identity);
    }
}
