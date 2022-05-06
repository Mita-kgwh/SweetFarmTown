using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Slider bar;

    public void Set(int cur, int max)
    {
        bar.maxValue = max;
        bar.value = cur;

        text.text = cur.ToString() + "/" + max.ToString();
    }
}
