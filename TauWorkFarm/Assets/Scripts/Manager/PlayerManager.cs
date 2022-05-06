using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    public int maxVal;
    public int curVal;

    public Stat(int max, int cur)
    {
        maxVal = max;
        curVal = cur;
    }

    internal void Subtract(int amount)
    {
        curVal -= amount;
    }

    internal void Add(int amount)
    {
        curVal += amount;
        if (curVal > maxVal)
        {
            curVal = maxVal;
        }
    }

    internal void SetToMax()
    {
        curVal = maxVal;
    }
}

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerManager>();
            return instance;
        }
    }

    public Stat hp;
    [SerializeField] StatusBar hpBar;
    public Stat stamina;
    [SerializeField] StatusBar staminaBar;

    public bool isDead;
    public bool isExhausted;

    private void Start()
    {
        UpdateHPBar();
        UpdateStaminaBar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(10);
        }
    }

    private void UpdateHPBar()
    {
        hpBar.Set(hp.curVal, hp.maxVal);
    }
    private void UpdateStaminaBar()
    {
        staminaBar.Set(stamina.curVal, stamina.maxVal);
    }
    public void TakeDamage(int amount)
    {
        hp.Subtract(amount);
        if (hp.curVal < 0)
        {
            isDead = true;
        }
        UpdateHPBar();
    }
    public void Heal(int amount)
    {
        hp.Add(amount);
        UpdateHPBar();
    }

    public void FullHeal()
    {
        hp.SetToMax();
        UpdateHPBar();
    }

    public void GetTired(int amount)
    {
        stamina.Subtract(amount);

        if (stamina.curVal < 0)
        {
            isExhausted = true;
        }
        UpdateStaminaBar();
    }

    public void Rest(int amount)
    {
        stamina.Add(amount);
        UpdateStaminaBar();
    }

    public void FullRest()
    {
        stamina.SetToMax();
        UpdateStaminaBar();
    }
}
