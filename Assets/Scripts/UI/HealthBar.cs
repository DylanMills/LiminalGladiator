using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    PlayerAttributes player;
    RectTransform fillTrans;
    Image barImage;

    [SerializeField] Sprite barSpriteNormal;
    [SerializeField] Sprite barSpriteDamaged;

    float maxWidth;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttributes>();
        fillTrans = transform.GetChild(0).GetComponent<RectTransform>();
        barImage = GetComponent<Image>();

        maxWidth = fillTrans.rect.width;
    }

    void FixedUpdate()
    {
        
    }
}
