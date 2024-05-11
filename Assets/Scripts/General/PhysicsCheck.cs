using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public float checkRadius;       // ���뾶
    public Vector2 bottomOffset;    // �ײ�ƫ��

    public LayerMask groundLayer;   // �����
    public bool isGround;

    private void Update()
    {
        Check();
    }

    public void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);   // ������
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    }
}
