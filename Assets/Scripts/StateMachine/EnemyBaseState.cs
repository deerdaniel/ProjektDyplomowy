using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateMachine enemy);
    public abstract void EnterState(EnemyStateMachine enemy, GameObject obj);
    public abstract void UpdateState(EnemyStateMachine enemy);
    public abstract void OnCollisionEnter(EnemyStateMachine enemy, Collision collision);
    public abstract void OnTriggerStay(EnemyStateMachine enemy, Collider collider);
}
