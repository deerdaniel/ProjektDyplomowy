using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowState : EnemyBaseState
{
    GameObject player;
    float distance;
    private NavMeshAgent agent;
    public override void EnterState(EnemyStateMachine enemy)
    {
        
    }
    public override void EnterState(EnemyStateMachine enemy, GameObject obj)
    {
        agent = enemy.GetComponent<NavMeshAgent>();
        player = obj;
    }
    public override void UpdateState(EnemyStateMachine enemy)
    {
        agent.stoppingDistance = 2.0f;
        agent.destination = player.transform.position;
        //enemy.transform.LookAt(player.transform.position);
        ////enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, player.transform.position, enemy.RunSpeed * Time.deltaTime);
        //distance = Vector3.Distance(enemy.transform.position, player.transform.position);
        //if (2.0f > distance)
        //{
        //    enemy.SwitchState(enemy.EnemyAttact);
        //}

    }
    public override void OnCollisionEnter(EnemyStateMachine enemy, Collision collision)
    {

    }
    public override void OnTriggerStay(EnemyStateMachine enemy, Collider collider)
    {

    }
}
