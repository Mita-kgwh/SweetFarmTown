using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreSlot_UI : Slot_UI, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI priceText;

    public override void SetItem(Slot slot)
    {
        base.SetItem(slot);
        nameText.text = slot.item.Name;
        priceText.text = slot.item.price.ToString();
    }

    public override void SetEmpty()
    {
        base.SetEmpty();
        nameText.text = "";
        priceText.text = "";
    }
}
