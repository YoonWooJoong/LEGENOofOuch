using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera thisCam;
    [SerializeField] private CinemachineConfiner2D confiner;
    [SerializeField] private Collider2D confinerCollider;

    private void Awake()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
        confinerCollider = transform.parent.GetComponent<StageContainer>().cameraCollider;
        GameManager.Instance.CameraManager.SetVirtualCam(thisCam);
    }

    /// <summary>
    /// 타일맵 프리팹이 활성화될때 해당 타일맵의 버츄얼카메라 세팅
    /// </summary>
    private void Start()
    {
        transform.GetComponent<CinemachineVirtualCamera>().Follow = GameManager.Instance.player.transform;
        SetupConfiner();
    }

    /// <summary>
    /// 시네머신의 Confiner 기능을 사용하기위한 콜라이더 참조 설정
    /// </summary>
    public void SetupConfiner()
    {
        if (confinerCollider == null)
        {
            return;
        }
        confiner.m_BoundingShape2D = confinerCollider;
        confiner.InvalidateCache(); // 변경 사항 반영
    }
}
