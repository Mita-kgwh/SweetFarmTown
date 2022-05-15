using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Currency : MonoBehaviour
{
    [SerializeField] int amout;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        amout = 1000;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = amout.ToString();
    }

    public void Add(int moneyGain)
    {
        amout += moneyGain;
        UpdateText();
    }

    public bool Check(int totalPrice)
    {
        return amout >= totalPrice;
    }

    internal void Decrease(int totalPrice)
    {
        amout -= totalPrice;
        if (amout < 0) { amout = 0; }
        UpdateText();
    }
}
