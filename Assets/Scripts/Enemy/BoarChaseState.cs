using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boar ׷��״̬
/// </summary>
public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        Debug.Log("Chase");
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;    // ׷���ٶ�
        currentEnemy.anim.SetBool("run", true);
    }

    public override void LogicUpdate()
    {
        // ��ʧʱ�����
        if (currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwitchState(NPCState.Patrol);  // �л�Ѳ��״̬
        }

        // ���ڵ����ײǽ
        if (!currentEnemy.physicsCheck.isGround || currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0 || currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0)
        {
            currentEnemy.TurnBack();    // ת��
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
