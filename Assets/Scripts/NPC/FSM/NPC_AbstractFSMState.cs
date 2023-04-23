using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NPC_ExecutionState
{
    NONE,
    ACTIVE,
    COMPLETED,
    TERMINATED,
};

public enum NPC_FSMStateType
{
    IDLE,
    PATROL,
};

public abstract class NPC_AbstractFSMState : ScriptableObject
{
    protected NPC npc;
    protected NPC_FiniteStateMachine finiteStateMachine;

    public NPC_ExecutionState executionState { get; protected set; }
    public NPC_FSMStateType stateType { get; protected set; }
    public bool enteredState { get; protected set; }

    public virtual void OnEnable()
    {
        executionState = NPC_ExecutionState.NONE;
    }

    public virtual bool EnterState()
    {
        bool successNavMesh = true;
        bool successNPC= true;
        executionState = NPC_ExecutionState.ACTIVE;

        successNPC = (npc != null);

        return successNavMesh & successNPC;
    }

    public abstract void UpdateState();


    public virtual void ExitState()
    {
        executionState = NPC_ExecutionState.COMPLETED;

    }

    public virtual void SetExecutingNPC(NPC _npc)
    {
        if (_npc != null)
        {
            npc = _npc;
        }
    }

    public virtual void SetExecutingFSM(NPC_FiniteStateMachine _finiteStateMachine)
    {
        if (_finiteStateMachine != null)
        {
            finiteStateMachine = _finiteStateMachine;
        }
    }
}
