using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysicsCheck physicsCheck;

    [Header("Base Attributes")]
    public float normalSpeed;   // �ƶ��ٶ�
    public float chaseSpeed;    // ׷���ٶ�
    [HideInInspector] public float currentSpeed;  // ��ǰ�ٶ�

    public Vector3 faceDir;
    public float hurtForce;

    public Transform attacker;

    [Header("Check")]
    public Vector2 checkOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;

    [Header("Timer")]
    public float waitTime;         // �ȴ�ʱ��
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;         // ��ʧʱ��
    public float lostTimeCounter;

    [Header("States")]
    public bool isHurt;
    public bool isDead;

    protected BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();

        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;
    }

    private void OnEnable()
    {
        GetComponent<Character>().OnTakeDamage.AddListener(Hurt);   // ��������¼�����

        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);

        currentState.LogicUpdate();

        TimeCounter();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isDead && !wait)
        {
            Move();
        }

        currentState.PhysicsUpdate();
    }

    private void OnDisable()
    {
        currentState.OnExit();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(faceDir.x * currentSpeed * Time.deltaTime, rb.velocity.y);
    }

    public void TimeCounter()
    {
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                TurnBack();    // ��ת
            }
        }

        // ��ʧ����Ŀ��
        if (!FoundPlayer())
        {
            if (lostTimeCounter > 0)
                lostTimeCounter -= Time.deltaTime;
        }
        else
        {
            lostTimeCounter = lostTime;
        }
    }

    public void TurnBack()
    {
        transform.localScale = new Vector3(faceDir.x, 1, 1);    // ��ת
    }

    public bool FoundPlayer()
    {
        return Physics2D.BoxCast((Vector2)transform.position + checkOffset, checkSize, 0, faceDir, checkDistance, attackLayer);
    }

    /// <summary>
    /// �л�״̬
    /// </summary>
    /// <param name="state">NPC ״̬</param>
    public void SwitchState(NPCState state)
    {
        // ��״̬
        var newState = state switch
        {
            NPCState.Patrol => patrolState, // Ѳ��
            NPCState.Chase => chaseState,   // ׷��
            _ => null
        };

        currentState.OnExit();      // �˳���ǰ״̬
        currentState = newState;    // �л���״̬
        currentState.OnEnter(this); // ������״̬
    }

    #region EventFunctions
    public void Hurt(Transform attacker)
    {
        this.attacker = attacker;
        // ��ת
        if (attacker.transform.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (attacker.transform.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        isHurt = true;
        anim.SetTrigger("hurt");

        rb.velocity = new Vector2(0, rb.velocity.y);    // ֹͣ x �ƶ�

        StartCoroutine(OnHurt());
    }

    private IEnumerator OnHurt()
    {
        rb.AddForce((transform.position - attacker.position).normalized * hurtForce, ForceMode2D.Impulse);   // ��ӷ�����
        yield return new WaitForSeconds(0.45f);
        isHurt = false;
    }

    public void Die()
    {
        gameObject.layer = 2;

        anim.SetBool("dead", true);
        isDead = true;
    }

    public void DestoryAfterAnimation()
    {
        Destroy(gameObject);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)checkOffset + new Vector3(checkDistance * -transform.localScale.x, 0), 0.2f);
    }
}
