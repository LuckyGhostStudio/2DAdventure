using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraController : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;
    public CinemachineImpulseSource impulseSource;      // 振动源

    public VoidEventSO cameraShakeEvent;    // 相机振动事件

    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
    }

    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
    }

    private void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();    // 产生振动
    }

    // TODO: 场景切换后处理
    private void Start()
    {
        SetNewCameraBounds();
    }

    /// <summary>
    /// 设置 Camera 边界碰撞体
    /// </summary>
    private void SetNewCameraBounds()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Bounds");
        if (!obj) return;

        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();  // 设置边界碰撞体
        confiner2D.InvalidateCache();                                   // 清除缓存
    }
}
