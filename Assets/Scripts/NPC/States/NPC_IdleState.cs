using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NPC_IdleState", menuName = "Unity-FSM/NPC/Idle", order = 1)]
public class NPC_IdleState : NPC_AbstractFSMState
{
    float totalDuration;

    public override void OnEnable()
    {
        base.OnEnable();
        stateType = NPC_FSMStateType.IDLE;
    }

    public override bool EnterState()
    {
        enteredState = false;

        if (base.EnterState())
        {
            Debug.Log("Enter Idle State");
            npc.npcAnimator.SetBool("Run", false);
            totalDuration = 0;
            enteredState = true;
        }

        return enteredState;
    }

    public override void UpdateState()
    {
        if (enteredState == true)
        {
            if(npc.dialogueInProgress==false)
            {
                totalDuration += Time.deltaTime;

                if (totalDuration >= npc.idleDuration)
                {
                    finiteStateMachine.EnterStateType(NPC_FSMStateType.PATROL);
                }
            }                 
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit Idle State");
        base.ExitState();
    }
}
