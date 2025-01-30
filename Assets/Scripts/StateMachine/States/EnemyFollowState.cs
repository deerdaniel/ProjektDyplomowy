using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowState : EnemyBaseState
{
    GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    private float distance;
    public override void EnterState(EnemyStateMachine enemy)
    {
        
    }
    public override void EnterState(EnemyStateMachine enemy, GameObject obj)
    {
        player = obj;
        animator = enemy.GetComponent<Animator>();
        agent = enemy.GetComponent<NavMeshAgent>();
        agent.speed = enemy.RunSpeed;
        agent.acceleration = 40f;
        agent.angularSpeed = 120f;
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsRunning", true);
        //agent.enabled = true;
        agent.stoppingDistance = 0.7f;
        
        //agent.destination = player.transform.position;

    }
    public override void UpdateState(EnemyStateMachine enemy)
    {
        distance = Vector3.Distance(enemy.transform.position, player.transform.position);
        agent.SetDestination(player.transform.position);
        if (distance <= agent.stoppingDistance)
        {
            agent.SetDestination(enemy.transform.position);
            enemy.SwitchState(enemy.EnemyAttact, player);
        }

    }

    public override void OnTriggerEnter(EnemyStateMachine enemy, Collider collider)
    {

    }
    public override void OnTriggerStay(EnemyStateMachine enemy, Collider collider)
    {

    }
}
