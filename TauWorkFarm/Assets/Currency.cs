using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Currency : MonoBehaviour
{
    [SerializeField] int playerMoney;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        playerMoney = 1000;
        UpdateText();
    }

    public int GetMoney()
    {
        return playerMoney;
    }

    private void UpdateText()
    {
        text.text = playerMoney.ToString();
    }

    public void Add(int moneyGain)
    {
        playerMoney += moneyGain;
        UpdateText();
    }

    public bool Check(int totalPrice)
    {
        return playerMoney >= totalPrice;
    }

    public void Decrease(int totalPrice)
    {
        playerMoney -= totalPrice;
        if (playerMoney < 0) { playerMoney = 0; }
        UpdateText();
    }

    internal void SetValue(int money)
    {
        playerMoney = money;
    }
}
