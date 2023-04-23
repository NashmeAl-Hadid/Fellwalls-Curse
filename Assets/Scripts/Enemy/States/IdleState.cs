using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "IdleState", menuName = "Unity-FSM/States/Idle", order = 1)]
public class IdleState : AbstractFSMState
{
    float totalDuration;

    public override void OnEnable()
    {
        base.OnEnable();
        stateType = FSMStateType.IDLE;
    }

    public override bool EnterState()
    {
        enteredState = false;

        if (base.EnterState())
        {
            Debug.Log("Enter Idle State");
            enemy.enemyAnimator.SetBool("Run", false);
            totalDuration = 0;
            enteredState = true;
        }

        return enteredState;
    }

    public override void UpdateState()
    {
        if (enteredState == true)
        {
            totalDuration += Time.deltaTime;

            if (enemy.chase == true && enemy.RangeAttackCheck() == false && enemy.MeleeAttackCheck() == false)
            {
                //if(enemy.isKnockbackHappening == false)
                //{
                    finiteStateMachine.EnterStateType(FSMStateType.CHASE);
                //}                        
            }
            else
            {
                if (enemy.canMeleeAttack == true && enemy.MeleeAttackCheck())
                {
                    if(enemy.meleeAttackTimer <= 0)
                    {
                        finiteStateMachine.EnterStateType(FSMStateType.MELEEATTACK);
                    }                                 
                }
                else if (enemy.canRangeAttack == true && enemy.RangeAttackCheck())
                {
                    if(enemy.rangeAttackTimer <= 0)
                    {
                        finiteStateMachine.EnterStateType(FSMStateType.RANGEATTACK);
                    }                    
                }
                else if (totalDuration >= 3.5f)
                {
                    if(enemy.MeleeAttackCheck() == false && enemy.TargetDetected()==false)
                    {
                        finiteStateMachine.EnterStateType(FSMStateType.PATROL);
                    }
                    else
                    {
                        totalDuration = 0;
                    }
                   
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
