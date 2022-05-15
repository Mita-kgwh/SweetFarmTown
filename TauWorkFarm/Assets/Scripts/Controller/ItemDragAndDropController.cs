using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragAndDropController : MonoBehaviour
{
    public Slot slot;
    [SerializeField] GameObject dragIcon;
    [SerializeField] Image image;
    [SerializeField] RectTransform iconTransform;

    private void Start()
    {
        slot = new Slot();
    }

    private void Update()
    {
        if (dragIcon.activeInHierarchy)
        {
            
            iconTransform.position = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    //Debug.Log("Out");
                    // chuyen mouseposition thanh worldposition de spawn item
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldPosition.z = 0f; //tranh viec spawn phia sau cameraa
                    ItemSpawnManager.Instance.SpawnItem(worldPosition, slot.item, slot.count);

                    slot.Clear();
                    dragIcon.SetActive(false);
                }
            }
        }
    }

    public bool CheckForSale()
    {
        if (slot.item == null) { return false; }
        if (!slot.item.canBeSold) { return false; }

        return true;
    }

    internal void RemoveItem(int count = 1)
    {
        if (slot == null) { return; }

        if (slot.item.stackable)
        {
            slot.count -= count;
            if (slot.count <= 0)
            {
                slot.Clear();
            }
        }
        else
        {
            slot.Clear();
        }
        UpdateIcon();
              
    }

    public bool Check(Item item, int count = 1)
    {
        if (slot == null) { return false; }

        if (item.stackable)
        {
            return slot.item == item && slot.count >= count;  
        }

        return slot.item == item;
    }

    internal void OnClick(Slot _slot)
    {
        //Debug.Log("Click Drag and Drop");
        if (slot.item == null)  // copy item tu trong ra ngoai slot dragicon
        {
            slot.Copy(_slot);
            _slot.Clear();
        }
        else                    // them item tu ngoai vao trong inventory 
        {
            if (slot.item == _slot.item)
            {
                _slot.count += slot.count;
                slot.Clear();
            }
            else
            {
                Item item = _slot.item;
                int count = _slot.count;

                _slot.Copy(slot);
                slot.Set(item, count);
            }  
        }
        UpdateIcon();
    }
    public void UpdateIcon()
    {
        if (slot.item == null)
        {
            dragIcon.SetActive(false);
        }
        else
        {
            dragIcon.SetActive(true);
            image.sprite = slot.item.icon;
        }

    }
}
