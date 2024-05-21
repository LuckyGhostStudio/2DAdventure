using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boar 追击状态
/// </summary>
public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        Debug.Log("Chase");
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;    // 追击速度
        currentEnemy.anim.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
        // 丢失时间结束
        if (currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);  // 切换巡逻状态
        }

        // 不在地面或撞墙
        if (!currentEnemy.physicsCheck.isGround || currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0 || currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0)
        {
            currentEnemy.TurnBack();    // 转身
        }
    }

    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.anim.SetBool("run", false);
    }
}
