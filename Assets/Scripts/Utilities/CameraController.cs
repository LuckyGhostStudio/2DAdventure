using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraController : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;
    public CinemachineImpulseSource impulseSource;      // ��Դ

    public VoidEventSO cameraShakeEvent;    // ������¼�

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
        impulseSource.GenerateImpulse();    // ������
    }

    // TODO: �����л�����
    private void Start()
    {
        SetNewCameraBounds();
    }

    /// <summary>
    /// ���� Camera �߽���ײ��
    /// </summary>
    private void SetNewCameraBounds()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Bounds");
        if (!obj) return;

        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();  // ���ñ߽���ײ��
        confiner2D.InvalidateCache();                                   // �������
    }
}
