using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    EnemyBaseState currentState;
    public Transform Target;
    public float WalkSpeed = 2.0f;
    public float RunSpeed = 4.0f;

    public EnemyPatrolState PatrolState = new();
    public EnemyFollowState FollowState = new();
    public EnemyAttactState EnemyAttact = new();
    public EnemyDeathState DeathState = new();
    //public GameObject player;
    [SerializeField]
    public CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        currentState = PatrolState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
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
}
