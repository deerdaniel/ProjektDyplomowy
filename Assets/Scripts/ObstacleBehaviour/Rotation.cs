using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float SpeedRotation = 0.5f;
    void Update()
    {
        transform.Rotate(0, SpeedRotation, 0);
    }
}
