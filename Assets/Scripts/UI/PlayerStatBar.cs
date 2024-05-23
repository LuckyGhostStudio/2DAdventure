using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;      // Ѫ���ӳ� Image
    public Image powerImage;

    private void Update()
    {
        // Ѫ���ӳٱ仯
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Ѫ���ı�ʱ����
    /// </summary>
    /// <param name="percentage">Ѫ���ٷֱ�</param>
    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount = percentage;
    }
}
