using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    [SerializeField] GameObject openDoor;
    [SerializeField] GameObject closeDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    private void CloseDoor()
    {
        closeDoor.SetActive(true);
        openDoor.SetActive(false);
    }

    private void OpenDoor()
    {
        closeDoor.SetActive(false);
        openDoor.SetActive(true);
    }
}
