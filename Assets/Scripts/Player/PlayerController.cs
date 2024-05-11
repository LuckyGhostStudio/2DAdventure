using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;

    public Vector2 inputDirection;  // ���뷽��

    private Rigidbody2D rb;
    private CapsuleCollider2D coll;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;

    [Header("Physics Materials")]
    public PhysicsMaterial2D normalMat;
    public PhysicsMaterial2D wallMat;

    [Header("Base Attributes")]
    public float speed;
    public float jumpForce;
    public float hurtForce;

    private Vector2 originalOffset;     // ��ײ�� Offset
    private Vector2 originalSize;       // ��ײ�� Size

    [Header("States")]
    public bool isCrouch;
    public bool isHurt;
    public bool isDead;
    public bool isAttack;

    private void Awake()
    {
        inputControl = new PlayerInputControl();

        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        coll = GetComponent<CapsuleCollider2D>();
        playerAnimation = GetComponent<PlayerAnimation>();

        originalOffset = coll.offset;
        originalSize = coll.size;

        inputControl.Gameplay.Jump.started += Jump;
        inputControl.Gameplay.Attack.started += Attack;
    }

    private void OnEnable()
    {
        inputControl.Enable();

        GetComponent<Character>().OnTakeDamage.AddListener(Hurt);   // ��������¼�����
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();

        CheckState();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        {
            Move();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }

    /// <summary>
    /// �ƶ�
    /// </summary>
    public void Move()
    {
        // �ƶ�
        if (!isCrouch)
        {
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        }

        // ���ҷ�ת
        int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0) faceDir = 1;
        if (inputDirection.x < 0) faceDir = -1;

        transform.localScale = new Vector3(faceDir, 1, 1);

        // �¶׼��
        isCrouch = inputDirection.y < -0.5f && physicsCheck.isGround;
        if (isCrouch)
        {
            rb.velocity = Vector2.zero;
            coll.offset = new Vector2(coll.offset.x, 0.85f);
            coll.size = new Vector2(coll.size.x, 1.7f);
        }
        else
        {
            // ��ԭ��ײ��
            coll.offset = originalOffset;
            coll.size = originalSize;
        }
    }

    /// <summary>
    /// ��Ծ
    /// </summary>
    /// <param name="context"></param>
    private void Jump(InputAction.CallbackContext context)
    {
        // �ڵ���
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="context"></param>
    private void Attack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayAttack();   //���Ź�������
        isAttack = true;
    }

    #region EventFunctions
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="attacker">������</param>
    public void Hurt(Transform attacker)
    {
        Debug.Log(attacker.name);
        isHurt = true;
        rb.velocity = Vector2.zero;

        rb.AddForce((transform.position - attacker.position).normalized * hurtForce, ForceMode2D.Impulse);   // ��ӷ�����
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Dead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();    // ���� Gameplay ����
    }
    #endregion

    private void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normalMat : wallMat;  // �����������
    }
}
