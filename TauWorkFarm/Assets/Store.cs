using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : Interactable
{
    public ItemContainer storeContainer;

    public float buyFromPlayerMultip = 0.5f;
    public float sellToPlayerMultip = 1.5f;

    Trading trading;

    public override void Interact(PlayerController player)
    {
        trading = player.GetComponent<Trading>();
        if (trading == null) { return; }
        //Debug.Log("interact sucess");
        trading.ButtonBuy(true).ButtonSell(true);
        trading.BeginTrading(this);
    }
}
