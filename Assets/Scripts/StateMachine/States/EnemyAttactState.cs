using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        //agent.enabled = false;
        //agent.stoppingDistance = 2.5f;
        player = obj;
    }
    public override void UpdateState(EnemyStateMachine enemy)
    {
        distance = Vector3.Distance(enemy.transform.position, player.transform.position);
        enemy.transform.LookAt(new Vector3( player.transform.position.x, 0, player.transform.position.z ));
        Debug.Log("distance: " + agent.remainingDistance);
        Debug.Log("Stoping distance" + agent.stoppingDistance);
        if (distance >= agent.stoppingDistance)
        {
            Debug.Log("stop");
            enemy.SwitchState(enemy.FollowState, player);
        }
    }
    public override void OnCollisionEnter(EnemyStateMachine enemy, Collision collision)
    {

    }
    public override void OnTriggerStay(EnemyStateMachine enemy, Collider collider)
    {

    }
}
