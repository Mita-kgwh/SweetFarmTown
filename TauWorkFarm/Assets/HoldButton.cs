using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] PlayerController playerController;

    public void OnPointerDown(PointerEventData eventData)
    {
        playerController.running = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerController.running = false;
    }

}
