using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private CinemachineVirtualCamera virtualCam;
    private const float fixedOrthoSize = 5f;
    private const float targetAspect = 16f / 9f;

    private void Start()
    {
        UpdateCamera();
    }

    private void Update()
    {
        if (Screen.width != Screen.currentResolution.width || Screen.height != Screen.currentResolution.height)
        {
            UpdateCamera();
        }

        // 카메라 사이즈 강제 유지
        if (cam.orthographicSize != fixedOrthoSize)
            cam.orthographicSize = fixedOrthoSize;

        if (virtualCam != null && virtualCam.m_Lens.OrthographicSize != fixedOrthoSize)
            virtualCam.m_Lens.OrthographicSize = fixedOrthoSize;
    }


    /// <summary>
    /// 현재 사용중인 버츄얼캠 설정
    /// </summary>
    public void SetVirtualCam(CinemachineVirtualCamera cam)
    {
        virtualCam = cam;
    }

    /// <summary>
    /// 16:9 비율 유지 (Letterbox 적용)
    /// </summary>
    private void UpdateCamera()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            cam.rect = new Rect(0, (1.0f - scaleHeight) / 2.0f, 1.0f, scaleHeight);
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            cam.rect = new Rect((1.0f - scaleWidth) / 2.0f, 0, scaleWidth, 1.0f);
        }
    }
}
