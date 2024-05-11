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

    public UnityEvent<Transform> OnTakeDamage;  // 受伤事件
    public UnityEvent OnDie;                    // 死亡事件

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // 无敌计时
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
    /// 计算受到的伤害
    /// </summary>
    /// <param name="attacker">攻击者</param>
    public void TakeDamage(AttackController attacker)
    {
        if (invulnerable) return;

        currentHealth -= attacker.damage;
        if(currentHealth > 0)
        {
            TriggerInvulnerable();  // 触发无敌

            OnTakeDamage?.Invoke(attacker.transform);   // 处理受伤事件
        }
        else
        {
            currentHealth = 0;

            OnDie?.Invoke();    // 处理死亡事件
        }

    }

    /// <summary>
    /// 触发无敌
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
