using System;
using Cinemachine;
using UnityEngine;

namespace JWOAGameSystem
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField, Tooltip("默认距离")][Range(0f, 10f)] private float defaultDistance = 6f;
        [SerializeField, Tooltip("最小距离"), Range(0f, 10f)] private float minimumDistance = 1f;
        [SerializeField, Tooltip("最大距离"), Range(0f, 10f)] private float maximumDistance = 6f;

        [SerializeField, Tooltip("平滑距离插值的值"), Range(0f, 10f)] private float smoothing = 4f;
        [SerializeField, Tooltip("与Z轴相乘的值(鼠标滚轮灵敏度)"), Range(0f, 10f)] private float zoomSensitivity = 1f;

        [Tooltip("cinemachine中Body的组件")] private CinemachineFramingTransposer framingTransposer;
        private CinemachineInputProvider inputProvider;

        [Tooltip("当前距离摄像机的距离")] private float currentTargetDistance;

        private void Awake()
        {
            framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
            inputProvider = GetComponent<CinemachineInputProvider>();

            currentTargetDistance = defaultDistance;
        }

        private void Update()
        {
            Zoom();
        }

        private void Zoom()
        {
            // 获取Z轴索引
            float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;

            // 限制玩家当前距离摄像机的距离大小
            currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minimumDistance, maximumDistance);

            float currentDistance = framingTransposer.m_CameraDistance;

            if (currentDistance == currentTargetDistance)
                return;

            // 计算平滑移动位置
            float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

            framingTransposer.m_CameraDistance = lerpedZoomValue;
        }
    }
}
