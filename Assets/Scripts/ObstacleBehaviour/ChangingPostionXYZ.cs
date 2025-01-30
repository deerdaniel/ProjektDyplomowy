using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChangingPostionXYZ : MonoBehaviour
{
    public int TargetPositionInX = 0;
    public int TargetPositionInY = 0;
    public int TargetPositionInZ = 0;
    public float Speed;

    private Vector3[] wayPoints = new Vector3[2];
    private int wayPointCounter = 1;

    void Start()
    {
        wayPoints[0] = transform.position;
        wayPoints[1] = transform.position + new Vector3(TargetPositionInX, TargetPositionInY, TargetPositionInZ);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[wayPointCounter], Speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wayPoints[wayPointCounter]) < 0.1f)
        {
            wayPointCounter = (wayPointCounter + 1) % 2;
        }
    }
}
