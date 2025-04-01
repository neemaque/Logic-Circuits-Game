using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShake : MonoBehaviour
{
    [SerializeField] private RectTransform uiElement;
    [SerializeField] private float intensity = 2f;
    [SerializeField] private float speed = 2f;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = uiElement.anchoredPosition;
        speed += Random.Range(-0.3f, 0.3f);
    }

    void Update()
    {
        float shakeX = Mathf.Sin(Time.time * speed) * intensity;
        float shakeY = Mathf.Cos(Time.time * speed) * intensity;
        uiElement.anchoredPosition = originalPosition + new Vector3(shakeX, shakeY, 0);
    }
}
