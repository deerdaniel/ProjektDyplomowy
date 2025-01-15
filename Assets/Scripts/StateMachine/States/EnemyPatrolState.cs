using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    Vector3 originPosition;
    Vector3 targetPosition;
    Vector3 currentDirection;
    public override void EnterState(EnemyStateMachine enemy)
    {
        enemy.animator.SetBool("IsPatroling", true);
        originPosition = enemy.transform.position;
        targetPosition = new Vector3(enemy.Target.position.x, enemy.Target.position.y, enemy.Target.position.z);
    }
    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (enemy.transform.position == targetPosition)
        {
            enemy.transform.LookAt(originPosition);
            currentDirection = originPosition;
        }
       else if (enemy.transform.position == originPosition)
        {
            enemy.transform.LookAt(targetPosition);
            currentDirection = targetPosition;
        }
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, currentDirection, enemy.WalkSpeed * Time.deltaTime);
    }
    public override void OnCollisionEnter(EnemyStateMachine enemy, Collision collision)
    {

    }
}
