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

public class PlayerManager : MonoBehaviour,IDamageable
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

    DisableControls disableControls;
    PlayerRespawn playerRespawn;

    private void Awake()
    {
        disableControls = GetComponent<DisableControls>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

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
        if (Input.GetKeyDown(KeyCode.G))
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
        if (isDead) { return; }
        hp.Subtract(amount);
        if (hp.curVal < 0)
        {
            Dead();
        }
        UpdateHPBar();
    }

    private void Dead()
    {
        isDead = true;
        disableControls.DisableControl();
        playerRespawn.StartRespawn();
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
            Exhausted();
        }
        UpdateStaminaBar();
    }

    private void Exhausted()
    {
        isExhausted = true;
        disableControls.DisableControl();
        playerRespawn.StartRespawn();
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

    public void CalculateDamage(ref int damage)
    {
        damage -= 2;
    }

    public void ApplyDamage(int damage)
    {
        TakeDamage(damage);
    }

    public void CheckState()
    {
        
    }
}
