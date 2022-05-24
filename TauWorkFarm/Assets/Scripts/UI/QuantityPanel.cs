using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuantityPanel : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] TextMeshProUGUI totalPriceText;
    [SerializeField] GameObject BuyButton;
    [SerializeField] GameObject SellButton;
    [SerializeField] GameObject DecreaseButton;
    [SerializeField] GameObject IncreaseButton;

    public int count = 1;
    public int max = 1;
    int singlePrice = 1;
    public bool one;
    public bool ten;
    public bool hundred;

    public int SetSinglePrice
    {
        set
        {
            singlePrice = value;
        }
    }

    public int GetTotalPrice
    {
        get
        {
            return singlePrice * count;
        }
    }

    public Sprite SetIcon
    {
        set
        {
            itemIcon.sprite = value;

        }
    }

    public void Increase()
    {
        if (one)
        {
            count += 1;

        }else if (ten)
        {
            count += 10;
        }else if (hundred)
        {
            count += 100;
        }
        if (count > max && SellButton.activeInHierarchy)
        {
            count = max;
        }
        UpdateUI();
    }

    public void Decrease()
    {
        if (one)
        {
            count -= 1;

        }
        else if (ten)
        {
            count -= 10;
        }
        else if (hundred)
        {
            count -= 100;
        }
        if (count <= 0)
        {
            count = 1;
        }
        UpdateUI();
    }

    public QuantityPanel ShowAll(bool show)
    {
        gameObject.SetActive(show);
        return this;
    }

    public QuantityPanel ButtonSell(bool show)
    {
        SellButton.SetActive(show);
        return this;
    }

    public QuantityPanel ButtonBuy(bool show)
    {
        BuyButton.SetActive(show);
        return this;
    }

    public QuantityPanel ButtonDecrease(bool show)
    {
        DecreaseButton.SetActive(show);
        return this;
    }

    public QuantityPanel ButtonIncrease(bool show)
    {
        IncreaseButton.SetActive(show);
        return this;
    }

    public void UpdateUI()
    {
        quantityText.text = count.ToString();
        totalPriceText.text = GetTotalPrice.ToString();
    }

    private void OnDisable()
    {
        count = 1;
    }
}
