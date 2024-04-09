using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private float shakeTimer;
    private CinemachineVirtualCamera CMvcam;
    
    private void Awake()
    {
        Instance = this;
        CMvcam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin CMbmcp = CMvcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        CMbmcp.m_AmplitudeGain = intensity;

        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin CMbmcp = CMvcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                CMbmcp.m_AmplitudeGain = 0f;
            }
        }
    }
}