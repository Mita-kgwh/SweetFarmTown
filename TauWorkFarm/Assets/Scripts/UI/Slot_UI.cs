using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class Slot_UI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image itemIcon;
    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] Image highLightImage;

    int myIndex;

    //public InventoryPanel inventory;

    public void SetIndex(int index)
    {
        myIndex = index;
    }

    // Start is called before the first frame update
    public void SetItem(Slot slot)
    {
        //if (slot != null)
        //{
        //    itemIcon.sprite = slot.item.icon;
        //    itemIcon.color = new Color(1, 1, 1, 1);
        //    quantityText.text = slot.count.ToString();
        //}     
        if (slot.item.stackable == true)
        {
            itemIcon.sprite = slot.item.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.gameObject.SetActive(true);
            quantityText.text = slot.count.ToString();
        }
        else
        {
            quantityText.gameObject.SetActive(false);
        }

    }

    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //ItemContainer inventory = GamesManager.Instance.inventoryContainer;
        //GamesManager.Instance.dragAndDropController.OnClick(inventory.slots[myIndex]);
        //transform.parent.transform.parent.transform.parent.GetComponent<Inventory_UI>().ShowInventory();
        //inventory_UI.ShowInventory();
        ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>();
        //Debug.Log(myIndex + " Slot_UI");
        if (itemPanel != null)
        {
            //Debug.Log("itemPanel found");
            itemPanel.OnClick(myIndex);
        }
        else
        {
            //Debug.Log("itemPanel not found");
        }
            

        //inventory.OnClick(myIndex);
    }

    public void HighLight(bool highlight)
    {
        highLightImage.gameObject.SetActive(highlight);
    }
}
