using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

public class EnemyAttactState : EnemyBaseState
{
    NavMeshAgent agent;
    GameObject player;
    Animator animator;
    private float distance;
    public override void EnterState(EnemyStateMachine enemy)
    {
    }
    public override void EnterState(EnemyStateMachine enemy, GameObject obj)
    {
        agent = enemy.GetComponent<NavMeshAgent>();
        animator = enemy.GetComponent <Animator>();
        animator.SetBool("IsAttacking", true);
        player = obj;
    }
    public override void UpdateState(EnemyStateMachine enemy)
    {
        distance = Vector3.Distance(enemy.transform.position, player.transform.position);
        enemy.transform.LookAt(new Vector3( player.transform.position.x, 0, player.transform.position.z ));
        if (distance >= agent.stoppingDistance)
        {
            enemy.SwitchState(enemy.FollowState, player);
        }
    }
    public override void OnTriggerEnter(EnemyStateMachine enemy, Collider collider)
    {
        
    }
    public override void OnTriggerStay(EnemyStateMachine enemy, Collider collider)
    {

    }

}
