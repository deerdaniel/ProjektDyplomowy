using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Enemy;
    public Transform[] SpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in SpawnPoints)
        {
            SpawnEnemy(t);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnEnemy(Transform tran)
    {
        Instantiate(Enemy, tran.position, Quaternion.identity);
    }
}
