using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FireData
{
    public bool onFire;
    public int count;
}

public class FireInteractable : Interactable, IPersistant
{
    [SerializeField] LightController lightController;
    [SerializeField] GameObject fire;
    [SerializeField] FireData data;
    [SerializeField] Item itemToBurn;
    [SerializeField] int timeExtendPerItem;
    [SerializeField] Item itemToStartFire;

    ToolsBarController toolsBarController;
    private void Start()
    {
        if (data == null)
        {
            data = new FireData();
        }
        if (!data.onFire)
        {
            fire.SetActive(false);
            lightController.SetIntensity(0);
            lightController.enabled = false;
        }
    }

    internal FireData GetData()
    {
        return data;
    }

    public override void Interact(PlayerController player)
    {
        if (toolsBarController == null)
        {
            toolsBarController = player.gameObject.GetComponent<ToolsBarController>();
        }

        Slot itemSlot = toolsBarController.GetItemSlot;
        if (itemSlot.item == itemToStartFire)
        {
            if (!data.onFire)
            {
                fire.SetActive(true);
                lightController.enabled = true;
                data.onFire = true;
                TakeItem(itemSlot);
                toolsBarController.UpdateCount();
            }
        }
        if (itemSlot.item == itemToBurn)
        {
            if (data.onFire)
            {
                data.count -= timeExtendPerItem;
                TakeItem(itemSlot);
                toolsBarController.UpdateCount();
            }
        }
        
    }

    private void TakeItem(Slot itemSlot)
    {
        if (itemSlot.item.stackable)
        {
            //Debug.Log("Da tru");
            itemSlot.count -= 1;
            if (itemSlot.count <= 0)
            {
                itemSlot.Clear();
            }
        }
        else
        {
            itemSlot.Clear();
        }
    }

    public void Load(string jsonString)
    {
        data = JsonUtility.FromJson<FireData>(jsonString);
    }

    public string Read()
    {
        return JsonUtility.ToJson(data);
    }
}
