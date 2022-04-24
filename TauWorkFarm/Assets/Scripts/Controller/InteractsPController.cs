using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractsPController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Rigidbody2D rgbd2d;
    [SerializeField] float offsetDis = 1f;
    [SerializeField] float sizeOfInteractableArea = 0.7f;
    [SerializeReference] HighlightController highlightController;

    private void Update()
    {
        CheckHighLight();

        if (Input.GetMouseButtonDown(1))
        {
            Interacts();
        }
    }

    public void CheckHighLight()
    {
        Vector2 position = rgbd2d.position + player.lastDirection * offsetDis;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                highlightController.Highlight(hit.gameObject);
                return;
            }
        }

        highlightController.Hide();
    }

    private void Interacts()
    {
        Vector2 position = rgbd2d.position + player.lastDirection * offsetDis;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(player);
                break;
            }
        }
    }
}
