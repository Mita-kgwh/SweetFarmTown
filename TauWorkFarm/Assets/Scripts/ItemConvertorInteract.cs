using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemConvertorData
{
    public Slot itemSlot;
    public int timer;

    public ItemConvertorData()
    {
        itemSlot = new Slot();
    }
}

[RequireComponent(typeof(TimeAgent))]
public class ItemConvertorInteract : Interactable, IPersistant
{
    [SerializeField] Item convertableItem;
    [SerializeField] Item producedItem;
    [SerializeField] int producedItemCount = 1;

    [SerializeField] int timeToProcess = 5; //chunk of time

    ItemConvertorData data;

    [SerializeField] Animator animator;
    [SerializeField] TimeAgent timeAgent;

    private void Start()
    {
        if (timeAgent == null) { timeAgent = GetComponent<TimeAgent>(); }
        timeAgent.onTimeTick += ItemConvertProcess;
        if (data == null)
        {
            data = new ItemConvertorData();
        }   
        if (animator == null) { animator = GetComponent<Animator>(); }

        Animate();
    }

    private void ItemConvertProcess()
    {
        if (data.itemSlot == null) { return; }

        if (data.timer > 0)
        {
            data.timer -= 1;
            if (data.timer <= 0)
            {
                //Debug.Log("Done");
                CompleteItemConvertion();
            }
        }
    }

    public override void Interact(PlayerController player)
    {
        if (data.itemSlot.item == null)
        {
            if (GamesManager.Instance.dragAndDropController.Check(convertableItem))
            {
                //Debug.Log("Start");
                StartItemProcessing(GamesManager.Instance.dragAndDropController.slot);
                return;
            }
                
            ToolsBarController toolsBarController = player.GetComponent<ToolsBarController>();    
            if (toolsBarController == null) { return; }
            //Debug.Log("toolbar not null");

            Slot slot = toolsBarController.GetItemSlot;

            if (slot.item == convertableItem)
            {
                StartItemProcessing(slot);
                toolsBarController.UpdateCount();
                return;
            }
        }
        
        if (data.itemSlot.item != null && data.timer <= 0)
        {
            GamesManager.Instance.inventoryContainer.Add(data.itemSlot.item, data.itemSlot.count);
            data.itemSlot.Clear();
        }

    }

    private void StartItemProcessing(Slot slotToProcess)
    {
        data.itemSlot.Copy(GamesManager.Instance.dragAndDropController.slot);
        data.itemSlot.count = 1;
        //GamesManager.Instance.dragAndDropController.RemoveItem();

        if (slotToProcess.item.stackable)
        {
            //Debug.Log("Da tru");
            slotToProcess.count -= 1;
            if (slotToProcess.count <= 0)
            {
                slotToProcess.Clear();
            }
        }
        else
        {
            slotToProcess.Clear();
        }

        data.timer = timeToProcess;
        Animate();
    }

    private void Animate()
    {
        animator.SetBool("Working", data.timer > 0f);
    }

    private void CompleteItemConvertion()
    {
        Animate();
        data.itemSlot.Clear();
        data.itemSlot.Set(producedItem, producedItemCount);
    }

    public string Read() 
    {
        return JsonUtility.ToJson(data);
    }

    public void Load(string jsonString)
    {
        data = JsonUtility.FromJson<ItemConvertorData>(jsonString);
    }
}
