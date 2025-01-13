using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    Vector3 SpawnPoint;
    Vector3 PointA;
    Vector3 PointB;
    public override void EnterState(EnemyStateMachine enemy)
    {

    }
    public override void UpdateState(EnemyStateMachine enemy)
    {
        enemy.characterController.Move(Vector3.forward * Time.deltaTime);
    }
    public override void OnCollisionEnter(EnemyStateMachine enemy, Collision collision)
    {

    }
}
