using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] PlayerController playerController;
    [SerializeField] float holdCount = 1f;
    private bool pressed;
    private float timer;

    private void Update()
    {
        if (pressed)
        {
            timer += Time.deltaTime;
            if (timer >= holdCount)
            {
                PlayerManager.Instance.GetTired(1);
                timer = 0;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("press");
        playerController.running = true;
        pressed = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
        playerController.running = false;
        timer = 0;
    }

}
