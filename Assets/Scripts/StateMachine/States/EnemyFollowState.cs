using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowState : EnemyBaseState
{
    GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    public override void EnterState(EnemyStateMachine enemy)
    {
        
    }
    public override void EnterState(EnemyStateMachine enemy, GameObject obj)
    {
        player = obj;
        animator = enemy.GetComponent<Animator>();
        agent = enemy.GetComponent<NavMeshAgent>();

        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsRunning", true);
        //agent.enabled = true;
        agent.stoppingDistance = 1.0f;
        
        //agent.destination = player.transform.position;

    }
    public override void UpdateState(EnemyStateMachine enemy)
    {
        agent.SetDestination(player.transform.position);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            enemy.SwitchState(enemy.EnemyAttact, player);
        }

    }
    public override void OnCollisionEnter(EnemyStateMachine enemy, Collision collision)
    {

    }
    public override void OnTriggerStay(EnemyStateMachine enemy, Collider collider)
    {

    }
}
