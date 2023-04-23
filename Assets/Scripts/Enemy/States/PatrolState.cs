using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PatrolState", menuName = "Unity-FSM/States/Patrol", order = 2)]
public class PatrolState : AbstractFSMState
{
    float totalDuration;

    public override void OnEnable()
    {
        base.OnEnable();
        stateType = FSMStateType.PATROL;
    }

    public override bool EnterState()
    {
        enteredState = false;

        if (base.EnterState())
        {
            Debug.Log("Enter Patrol State");
            enemy.enemyAnimator.SetBool("Run", true);
            enteredState = true;
        }

        return enteredState;
    }

    public override void UpdateState()
    {
        if (enteredState == true)
        {

            if (enemy.chase == true)
            {
                Debug.Log("Entering Chase State");
                finiteStateMachine.EnterStateType(FSMStateType.CHASE);
            }
            else
            {
                if (enemy.canMeleeAttack == true && enemy.MeleeAttackCheck())
                {
                    finiteStateMachine.EnterStateType(FSMStateType.MELEEATTACK);
                }
                else if (enemy.canRangeAttack == true && enemy.RangeAttackCheck())
                {
                    if (enemy.rangeAttackTimer <= 0)
                    {
                        finiteStateMachine.EnterStateType(FSMStateType.RANGEATTACK);
                    }
                }
                else if (enemy.NormalPatrolCheck() == true)
                {
                    enemy.NormalPatrol();
                }
                else
                {
                    finiteStateMachine.EnterStateType(FSMStateType.IDLE);
                }
            }

            //if (enemy.canJump==false)
            //{
              
            //}
            //else
            //{
            //    Jump();
            //}          

        }
    }
  
    //void Jump()
    //{
    //    enemy.rb2d.velocity = Vector3.zero;
    //    enemy.rb2d.velocity = Vector2.up * enemy.jumpPowerY;
    //    enemy.rb2d.velocity = new Vector2(enemy.enemyGameObject.transform.right.x * enemy.jumpPowerX, enemy.rb2d.velocity.y);
    //    enemy.canJump = false;
    //}

    public override void ExitState()
    {
        Debug.Log("Exit Patrol State");
        enemy.enemyAnimator.SetBool("Run", false);
        base.ExitState();
    }
}
