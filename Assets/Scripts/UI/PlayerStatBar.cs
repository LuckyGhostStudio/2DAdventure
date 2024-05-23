using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;      // 血条延迟 Image
    public Image powerImage;

    private void Update()
    {
        // 血条延迟变化
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 血量改变时调用
    /// </summary>
    /// <param name="percentage">血量百分比</param>
    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount = percentage;
    }
}
