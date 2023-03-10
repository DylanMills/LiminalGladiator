using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ShockwaveDecal : MonoBehaviour
{
    public GameObject decal;
    public Material decalMat;
    public float fade;
    public float lightFade = 0f;
    public GameObject m_LightObject;
    public bool fading = false;
    HDAdditionalLightData lightData;
    float startBright;
    // Start is called before the first frame update
    void Start()
    {
        fade = 0;
        lightData = m_LightObject.GetComponent<HDAdditionalLightData>();
        StartCoroutine(WaitTime());
        startBright = lightData.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (lightFade < 1)
        {
            lightFade += 0.2f;
            lightData.intensity = (1 - lightFade) * lightData.intensity;
        }
        if (fading)
        {
            fade += 0.1f;
            decalMat.SetFloat("_FadeAmount", fade);
        }
        if (!(fade < 1))
        {
            fading = false;
            decalMat.SetFloat("_FadeAmount", 0);
            Destroy(this.gameObject);
        }
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2);
        fading = true;
    }
}
