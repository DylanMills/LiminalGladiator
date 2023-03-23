using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cameraData;

    float shakeIntensity = 1f; //intensity of the screen shake

    private float originalFOV;  //original values
    private float originalTilt; //
    private float shakeTimer = 0f; //timer for the screen shake effect


    void Awake()
    {
        originalFOV = cameraData.m_Lens.FieldOfView;
        originalTilt = cameraData.m_Lens.Dutch;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            //generate random offset for screen shake effect, and add the original values to them
            float shakeFOV = Random.Range(-2f, 2f) * shakeIntensity + originalFOV;
            float shakeTilt = Random.Range(-1f, 1f) * shakeIntensity + originalTilt;

            //set the camera values
            SetCameraValues(shakeFOV, shakeTilt);

            //decrement the shake timer
            shakeTimer -= Time.deltaTime;

            if (shakeTimer <= 0)
            {
                // reset values when duration is up
                SetCameraValues(originalFOV, originalTilt);
            }
        }
    }

    private void SetCameraValues(float fov, float tilt)
    {
        cameraData.m_Lens.FieldOfView = fov;
        cameraData.m_Lens.Dutch = tilt;
    }

    //call this function to trigger the screen shake effect
    public void ShakeScreen(float duration, float intensity = 1f)
    {
        shakeTimer = duration;
        shakeIntensity = intensity;
    }
}