using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    Vector3 originPosition;
    Vector3 targetPosition;
    Vector3 currentDirection;
    Animator animator;
    public override void EnterState(EnemyStateMachine enemy)
    {
        animator = enemy.GetComponent<Animator>();
        animator.SetBool("IsPatroling", true);
        originPosition = enemy.transform.position;
        targetPosition = new Vector3(enemy.Target.position.x, enemy.Target.position.y, enemy.Target.position.z);
    }
    public override void EnterState(EnemyStateMachine enemy, GameObject obj)
    {

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
    public override void OnTriggerStay(EnemyStateMachine enemy, Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            GameObject player = collider.gameObject;
            enemy.SwitchState(enemy.FollowState, player);
        }
    }
}
