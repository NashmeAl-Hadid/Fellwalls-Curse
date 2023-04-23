using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class FiniteStateMachine : MonoBehaviour
{
    AbstractFSMState currentState;

    [SerializeField] private List<AbstractFSMState> validStates = null;

    private List<AbstractFSMState> InstantiatedStates;

    Dictionary<FSMStateType, AbstractFSMState> fsmStates;

    public bool startIdle = true;

    public void Awake()
    {
        currentState = null;

        fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();

        InstantiatedStates = new List<AbstractFSMState>();

        Enemy enemy = this.GetComponent<Enemy>();

        foreach (AbstractFSMState state in validStates)
        {
            AbstractFSMState i = Object.Instantiate(state);

            InstantiatedStates.Add(i);
        }

        foreach (AbstractFSMState state in InstantiatedStates)
        {
            state.SetExecutingFSM(this);
            state.SetExecutingEnemy(enemy);
            fsmStates.Add(state.stateType, state);
        }
    }

    private void Start()
    {
       if(startIdle==true)
        {
            EnterStateType(FSMStateType.IDLE);
        }
        else
        {
            EnterStateType(FSMStateType.PATROL);
        }         
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }

    #region STATE MANAGEMENT

    public void EnterStateMachine(AbstractFSMState nextState)
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

    public void EnterStateType(FSMStateType stateType)
    {
        if (fsmStates.ContainsKey(stateType))
        {
            AbstractFSMState nextState = fsmStates[stateType];

            EnterStateMachine(nextState);
        }
    }
    #endregion
}
