using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;

    [Header("Check Param")]
    public bool manual;             // �ֶ����ü�����

    public float checkRadius;       // ���뾶
    
    public Vector2 bottomOffset;    // �ײ�ƫ��
    public Vector2 leftOffset;      // ���ƫ��
    public Vector2 rightOffset;     // �Ҳ�ƫ��

    public LayerMask groundLayer;   // �����

    [Header("States")]
    public bool isGround;
    public bool touchLeftWall;
    public bool touchRightWall;

    private void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (!manual)
        {
            leftOffset = new Vector2(-coll.bounds.size.x * 0.5f + transform.localScale.x * coll.offset.x, coll.offset.y);
            rightOffset = new Vector2(coll.bounds.size.x * 0.5f + transform.localScale.x * coll.offset.x, coll.offset.y);
        }

        Check();
    }

    public void Check()
    {
        // ������
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRadius, groundLayer);
        // ǽ�ڼ��
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}
