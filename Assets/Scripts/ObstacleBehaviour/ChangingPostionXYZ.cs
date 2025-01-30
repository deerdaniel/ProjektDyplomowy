using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChangingPostionXYZ : MonoBehaviour
{
    public int targetPositionInX = 0;
    public int targetPositionInY = 0;
    public int targetPositionInZ = 0;
    public float speed;
    Vector3[] wayPoints = new Vector3[2];
    Vector3 originalPosition;
    Vector3 targetPosition;
    int wayPointCounter = 1;
    // Start is called before the first frame update
    void Start()
    {
        wayPoints[0] = transform.position;
        wayPoints[1] = transform.position + new Vector3(targetPositionInX, targetPositionInY, targetPositionInZ);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[wayPointCounter], speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wayPoints[wayPointCounter]) < 0.1f)
        {
            wayPointCounter = (wayPointCounter + 1) % 2;
        }
    }
}
