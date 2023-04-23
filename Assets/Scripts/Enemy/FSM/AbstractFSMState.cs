using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ExecutionState
{
    NONE,
    ACTIVE,
    COMPLETED,
    TERMINATED,
};

public enum FSMStateType
{
    IDLE,
    PATROL,
    CHASE,
    MELEEATTACK,
    RANGEATTACK,
};

public abstract class AbstractFSMState : ScriptableObject
{
    protected Enemy enemy;
    protected FiniteStateMachine finiteStateMachine;

    public ExecutionState executionState { get; protected set; }
    public FSMStateType stateType { get; protected set; }
    public bool enteredState { get; protected set; }

    public virtual void OnEnable()
    {
        executionState = ExecutionState.NONE;
    }

    public virtual bool EnterState()
    {
        bool successNavMesh = true;
        bool successEnemy = true;
        executionState = ExecutionState.ACTIVE;

        successEnemy = (enemy != null);

        return successNavMesh & successEnemy;
    }

    public abstract void UpdateState();


    public virtual void ExitState()
    {
        executionState = ExecutionState.COMPLETED;

    }

    public virtual void SetExecutingEnemy(Enemy _enemy)
    {
        if (_enemy != null)
        {
            enemy = _enemy;
        }
    }

    public virtual void SetExecutingFSM(FiniteStateMachine _finiteStateMachine)
    {
        if (_finiteStateMachine != null)
        {
            finiteStateMachine = _finiteStateMachine;
        }
    }
}
