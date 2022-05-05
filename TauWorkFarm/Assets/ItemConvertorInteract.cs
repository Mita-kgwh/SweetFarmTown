using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConvertorInteract : Interactable
{
    [SerializeField] Item convertableItem;
    [SerializeField] Item producedItem;
    [SerializeField] int producedItemCount = 1;

    Slot itemSlot;

    [SerializeField] float timeToProcess = 5f;
    float timer;

    [SerializeField] Animator animator;

    private void Start()
    {
        itemSlot = new Slot();
        if (animator == null) { animator = GetComponent<Animator>(); }
    }

    public override void Interact(PlayerController player)
    {
        if (itemSlot.item == null)
        {
            if (GamesManager.Instance.dragAndDropController.Check(convertableItem))
            {
                StartItemProcessing();
            }
        }
        if (itemSlot.item != null && timer < 0f)
        {
            GamesManager.Instance.inventoryContainer.Add(itemSlot.item, itemSlot.count);
            itemSlot.Clear();
        }

    }

    private void StartItemProcessing()
    {
        animator.SetBool("Working", true);
        itemSlot.Copy(GamesManager.Instance.dragAndDropController.slot);
        GamesManager.Instance.dragAndDropController.RemoveItem();

        timer = timeToProcess;
    }

    private void Update()
    {
        if (itemSlot == null) { return; }

        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                //Debug.Log("Done");
                CompleteItemConvertion();
            }
        }
    }

    private void CompleteItemConvertion()
    {
        animator.SetBool("Working", false);
        itemSlot.Clear();
        itemSlot.Set(producedItem, producedItemCount);
    }
}
