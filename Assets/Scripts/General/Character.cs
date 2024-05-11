using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("Base Attributes")]
    public float maxHealth;
    public float currentHealth;

    [Header("Hurt Invulnerable")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;

    public UnityEvent<Transform> OnTakeDamage;  // �����¼�
    public UnityEvent OnDie;                    // �����¼�

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // �޵м�ʱ
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }

    /// <summary>
    /// �����ܵ����˺�
    /// </summary>
    /// <param name="attacker">������</param>
    public void TakeDamage(AttackController attacker)
    {
        if (invulnerable) return;

        currentHealth -= attacker.damage;
        if(currentHealth > 0)
        {
            TriggerInvulnerable();  // �����޵�

            OnTakeDamage?.Invoke(attacker.transform);   // ���������¼�
        }
        else
        {
            currentHealth = 0;

            OnDie?.Invoke();    // ���������¼�
        }

    }

    /// <summary>
    /// �����޵�
    /// </summary>
    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
