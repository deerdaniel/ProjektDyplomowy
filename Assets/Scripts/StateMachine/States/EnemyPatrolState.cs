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
        
        agent.stoppingDistance = 1.0f;
        animator.SetBool("IsPatroling", true);
        pointA = enemy.transform.position;
        pointB = new Vector3(enemy.Target.position.x, enemy.Target.position.y, enemy.Target.position.z);
        targetPoint = pointB;
        wayPoints[0] = pointA;
        wayPoints[1] = pointB;
        //agent.SetDestination(targetPosition);
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
            Debug.Log(targetPoint);
        }
        //if (agent.remainingDistance <= agent.stoppingDistance)
        //{
        //    agent.SetDestination(originPosition);
        //}
        //distance = Vector3.Distance(wayPoints[wayPointCounter], enemy.transform.position);
        //Debug.Log(wayPointCounter);
        //Debug.Log(distance);
        //if (distance <= 0.5f)
        //{
        //    wayPointCounter = (wayPointCounter + 1) % 2;
        //    Debug.DrawRay(wayPoints[(wayPointCounter + 1) % 2], Vector3.up, Color.red, 1.0f);

        //    if (wayPointCounter < 10) wayPointCounter = 1;
        //    //agent.destination = originPosition;
        //    //enemy.transform.LookAt(originPosition);
        //    //currentDirection = originPosition;

        //}
        //enemy.transform.Translate(wayPoints[wayPointCounter] * Time.deltaTime);
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, wayPoints[wayPointCounter], enemy.WalkSpeed * Time.deltaTime);
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
