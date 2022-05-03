using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragAndDropController : MonoBehaviour
{
    [SerializeField] Slot slot;
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

    internal void OnClick(Slot _slot)
    {
        //Debug.Log("Click Drag and Drop");
        if (slot.item == null)  // copy vao slot trong
        {
            slot.Copy(_slot);
            _slot.Clear();
        }
        else                    // lay tu slot do ra ngoai
        {
            Item item = _slot.item;
            int count = _slot.count;

            _slot.Copy(slot);
            slot.Set(item, count);
        }
        UpdateIcon();
    }
    private void UpdateIcon()
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
