using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemConvertorData
{
    public Slot itemSlot;
    public float timer;

    public ItemConvertorData()
    {
        itemSlot = new Slot();
    }
}

public class ItemConvertorInteract : Interactable, IPersistant
{
    [SerializeField] Item convertableItem;
    [SerializeField] Item producedItem;
    [SerializeField] int producedItemCount = 1;

    [SerializeField] float timeToProcess = 5f;

    ItemConvertorData data;

    [SerializeField] Animator animator;

    private void Start()
    {
        if (data == null)
        {
            data = new ItemConvertorData();
        }   
        if (animator == null) { animator = GetComponent<Animator>(); }
    }

    public override void Interact(PlayerController player)
    {
        if (data.itemSlot.item == null)
        {
            if (GamesManager.Instance.dragAndDropController.Check(convertableItem))
            {
                StartItemProcessing();
            }
        }
        if (data.itemSlot.item != null && data.timer < 0f)
        {
            GamesManager.Instance.inventoryContainer.Add(data.itemSlot.item, data.itemSlot.count);
            data.itemSlot.Clear();
        }

    }

    private void StartItemProcessing()
    {
        animator.SetBool("Working", true);
        data.itemSlot.Copy(GamesManager.Instance.dragAndDropController.slot);
        data.itemSlot.count = 1;
        GamesManager.Instance.dragAndDropController.RemoveItem();

        data.timer = timeToProcess;
    }

    private void Update()
    {
        if (data.itemSlot == null) { return; }

        if (data.timer > 0f)
        {
            data.timer -= Time.deltaTime;
            if (data.timer <= 0f)
            {
                //Debug.Log("Done");
                CompleteItemConvertion();
            }
        }
    }

    private void CompleteItemConvertion()
    {
        animator.SetBool("Working", false);
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
