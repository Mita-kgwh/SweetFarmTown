using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTint : MonoBehaviour
{
    [SerializeField]
    Color unTintedColor;
    [SerializeField]
    Color tintedColor;

    float time;
    public float speed = 0.5f;

    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Tint()
    {
        StopAllCoroutines();
        time = 0;
        StartCoroutine(TintScreen());
    }

    public void UnTint()
    {
        StopAllCoroutines();
        time = 0;
        StartCoroutine(UnTintScreen());
    }

    private IEnumerator TintScreen()
    {
        while (time < 1f)
        {
            time += Time.deltaTime * speed;
            time = Mathf.Clamp(time, 0, 1);
            Color color = image.color;
            color = Color.Lerp(unTintedColor, tintedColor, time);

            image.color = color;

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator UnTintScreen()
    {
        while (time < 1f)
        {
            time += Time.deltaTime * speed;
            time = Mathf.Clamp(time, 0, 1);
            Color color = image.color;
            color = Color.Lerp(tintedColor, unTintedColor, time);

            image.color = color;

            yield return new WaitForEndOfFrame();
        }
    }
}
