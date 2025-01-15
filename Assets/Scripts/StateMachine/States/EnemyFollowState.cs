using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : EnemyBaseState
{
    GameObject player;
    float distance;
    public override void EnterState(EnemyStateMachine enemy)
    {
    }
    public override void EnterState(EnemyStateMachine enemy, GameObject obj)
    {
        player = obj;
    }
    public override void UpdateState(EnemyStateMachine enemy)
    {
        enemy.transform.LookAt(player.transform.position);
        //enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, player.transform.position, enemy.RunSpeed * Time.deltaTime);
        distance = Vector3.Distance(enemy.transform.position, player.transform.position);
        if (0.7f > distance)
        {
            enemy.SwitchState(enemy.EnemyAttact);
        }

    }
    public override void OnCollisionEnter(EnemyStateMachine enemy, Collision collision)
    {

    }
    public override void OnTriggerStay(EnemyStateMachine enemy, Collider collider)
    {

    }
}
