using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] Transform cameraTrans;

    public float shakeDuration = 0.2f; //duration of the screen shake
    public float shakeIntensity = 0.5f; //intensity of the screen shake

    private Vector3 originalPos; //original position of the camera
    private float shakeTimer = 0f; //timer for the screen shake effect


    void Awake()
    {
        originalPos = cameraTrans.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0)
        {
            //generate random position offset for screen shake effect
            Vector3 shakePos = Random.insideUnitSphere * shakeIntensity;
            shakePos.z = originalPos.z; // keep the camera's original Z position

            //set the camera position to the original position plus the shake position offset
            cameraTrans.position = originalPos + shakePos;

            //decrement the shake timer
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            //reset the camera position to the original position
            cameraTrans.position = originalPos;
        }
    }

    //call this function to trigger the screen shake effect
    public void ShakeScreen()
    {
        shakeTimer = shakeDuration;
    }
}