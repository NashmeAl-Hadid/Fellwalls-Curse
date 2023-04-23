using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NPC_PatrolState", menuName = "Unity-FSM/NPC/Patrol", order = 2)]
public class NPC_PatrolState : NPC_AbstractFSMState
{
    public override void OnEnable()
    {
        base.OnEnable();
        stateType = NPC_FSMStateType.PATROL;
    }

    public override bool EnterState()
    {
        enteredState = false;

        if (base.EnterState())
        {
            Debug.Log("Enter Patrol State");
            npc.npcAnimator.SetBool("Run", true);

            enteredState = true;
        }

        return enteredState;
    }

    public override void UpdateState()
    {
        if (enteredState == true)
        {
            if(npc.dialogueInProgress==true)
            {
                finiteStateMachine.EnterStateType(NPC_FSMStateType.IDLE);
            }
            else if (npc.NormalPatrolCheck() == true)
            {
                npc.NormalPatrol();
            }
            else
            {
                finiteStateMachine.EnterStateType(NPC_FSMStateType.IDLE);
            }
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit Idle State");
        npc.npcAnimator.SetBool("Run", false);
        base.ExitState();
    }
}
