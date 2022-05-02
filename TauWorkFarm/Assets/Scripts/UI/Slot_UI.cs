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
        itemIcon.sprite = slot.item.icon;
        itemIcon.color = new Color(1, 1, 1, 1);

        if (slot.item.stackable == true)
        {          
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
        
        ItemPanel itemPanel = transform.parent.GetComponent<ItemPanel>();
        //Debug.Log(myIndex + " Slot_UI");
        //if (itemPanel != null)
        {
            //Debug.Log("itemPanel found");
            itemPanel.OnClick(myIndex);
        }
        //else
        //{
        //    //Debug.Log("itemPanel not found");
        //}
            
    }

    public void HighLight(bool highlight)
    {
        highLightImage.gameObject.SetActive(highlight);
    }
}
