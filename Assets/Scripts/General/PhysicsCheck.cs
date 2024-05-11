using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public float checkRadius;       // ¼ì²â°ë¾¶
    public Vector2 bottomOffset;    // µ×²¿Æ«ÒÆ

    public LayerMask groundLayer;   // µØÃæ²ã
    public bool isGround;

    private void Update()
    {
        Check();
    }

    public void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);   // µØÃæ¼ì²â
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
    }
}
