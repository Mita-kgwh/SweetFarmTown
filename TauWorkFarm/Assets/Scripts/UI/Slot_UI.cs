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

    ItemPanel itemPanel;

    public void SetItemPanel(ItemPanel source)
    {
        itemPanel = source;
    }

    public void SetIndex(int index)
    {
        myIndex = index;
    }

    // Start is called before the first frame update
    public virtual void SetItem(Slot slot)
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

    public virtual void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        itemPanel.OnClick(myIndex);
     
    }

    public void HighLight(bool highlight)
    {
        highLightImage.gameObject.SetActive(highlight);
    }
}
