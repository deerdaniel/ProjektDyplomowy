using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    EnemyBaseState currentState;
    public Transform Target;
    public float WalkSpeed = 5;
    public Animator animator;

    public EnemyPatrolState PatrolState = new();
    public EnemyFollowState FollowState = new();
    public EnemyAttactState EnemyAttact = new();
    public EnemyDeathState DeathState = new();

    [SerializeField]
    public CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
