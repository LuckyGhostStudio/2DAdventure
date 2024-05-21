using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;

    [Header("Check Param")]
    public bool manual;             // ÊÖ¶¯ÉèÖÃ¼ì²â²ÎÊý

    public float checkRadius;       // ¼ì²â°ë¾¶
    
    public Vector2 bottomOffset;    // µ×²¿Æ«ÒÆ
    public Vector2 leftOffset;      // ×ó²àÆ«ÒÆ
    public Vector2 rightOffset;     // ÓÒ²àÆ«ÒÆ

    public LayerMask groundLayer;   // µØÃæ²ã

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
        // µØÃæ¼ì²â
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(bottomOffset.x * transform.localScale.x, bottomOffset.y), checkRadius, groundLayer);
        // Ç½±Ú¼ì²â
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
