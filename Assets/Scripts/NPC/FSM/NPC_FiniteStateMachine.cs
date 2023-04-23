using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class NPC_FiniteStateMachine : MonoBehaviour
{
    NPC_AbstractFSMState currentState;

    [SerializeField] private List<NPC_AbstractFSMState> validStates = null;

    private List<NPC_AbstractFSMState> InstantiatedStates;

    Dictionary<NPC_FSMStateType, NPC_AbstractFSMState> fsmStates;

    public void Awake()
    {
        currentState = null;

        fsmStates = new Dictionary<NPC_FSMStateType, NPC_AbstractFSMState>();

        InstantiatedStates = new List<NPC_AbstractFSMState>();

       NPC npc = this.GetComponent<NPC>();

        foreach (NPC_AbstractFSMState state in validStates)
        {
            NPC_AbstractFSMState i = Object.Instantiate(state);

            InstantiatedStates.Add(i);
        }

        foreach (NPC_AbstractFSMState state in InstantiatedStates)
        {
            state.SetExecutingFSM(this);
            state.SetExecutingNPC(npc);
            fsmStates.Add(state.stateType, state);
        }
    }

    private void Start()
    {
        // Debug.Log("Initial State");
        EnterStateType(NPC_FSMStateType.IDLE);
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    #region STATE MANAGEMENT

    public void EnterStateMachine(NPC_AbstractFSMState nextState)
    {
        if (nextState == null)
        {
            return;
        }

        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = nextState;
        currentState.EnterState();
    }

    public void EnterStateType(NPC_FSMStateType stateType)
    {
        if (fsmStates.ContainsKey(stateType))
        {
            NPC_AbstractFSMState nextState = fsmStates[stateType];

            EnterStateMachine(nextState);
        }
    }
    #endregion
}
