using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{
    Vector3 pointA;
    Vector3 pointB;
    Vector3 targetPoint;
    Vector3 currentDirection;
    Vector3[] wayPoints = new Vector3[2];
    int wayPointCounter = 1;
    float distance;
    NavMeshAgent agent;
    Animator animator;
    public override void EnterState(EnemyStateMachine enemy)
    {
        animator = enemy.GetComponent<Animator>();
        agent = enemy.GetComponent<NavMeshAgent>();
        animator.SetBool("IsPatroling", true);
        
        pointA = enemy.transform.position;
        pointB = new Vector3(enemy.Target.position.x, enemy.Target.position.y, enemy.Target.position.z);

        targetPoint = pointB;
        wayPoints[0] = pointA;
        wayPoints[1] = pointB;
    }
    public override void EnterState(EnemyStateMachine enemy, GameObject obj)
    {

    }
    public override void UpdateState(EnemyStateMachine enemy)
    {
        enemy.transform.LookAt(wayPoints[wayPointCounter]);
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, wayPoints[wayPointCounter], enemy.WalkSpeed * Time.deltaTime);
        if(Vector3.Distance(enemy.transform.position, wayPoints[wayPointCounter]) < 0.1f)
        {
            wayPointCounter = (wayPointCounter + 1) % 2;
        }
    }
    public override void OnTriggerEnter(EnemyStateMachine enemy, Collider collider)
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
