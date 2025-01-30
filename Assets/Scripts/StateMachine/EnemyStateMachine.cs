using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    
    public Transform Target;
    public float WalkSpeed = 2.0f;
    public float RunSpeed = 4.0f;
    public CharacterController characterController;

    public EnemyPatrolState PatrolState = new();
    public EnemyFollowState FollowState = new();
    public EnemyAttactState EnemyAttact = new();
    public EnemyDeathState DeathState = new();

    private EnemyBaseState currentState;
    private BoxCollider EnemyHandCollider;

    // Start is called before the first frame update
    void Start()
    {
        EnemyHandCollider = GetComponentInChildren<BoxCollider>();
        currentState = PatrolState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    void OnTriggerEnter(Collider collider)
    {
        currentState.OnTriggerEnter(this, collider);
    }
    void OnTriggerStay(Collider collider)
    {
        currentState.OnTriggerStay(this, collider);
    }
    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
    public void SwitchState(EnemyBaseState state, GameObject obj)
    {
        currentState = state;
        state.EnterState(this, obj);
    }

    void enableAttack()
    {
        EnemyHandCollider.enabled = true;
    }
    void disableAttack()
    {
        EnemyHandCollider.enabled = false;
    }
}
