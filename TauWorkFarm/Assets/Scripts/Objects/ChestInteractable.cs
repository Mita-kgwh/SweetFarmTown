using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable
{
    [SerializeField] GameObject chestClosed;
    [SerializeField] GameObject chestOpened;
    [SerializeField] bool isOpened;
    [SerializeField] AudioClip onOpenAudio;
    [SerializeField] ItemContainer itemContainer;
    ItemContainerInteractController playerContainerInteractController;

    public override void Interact(PlayerController player)
    {
        if (!isOpened)
        {
            Open(player);
        }
        else
        {
            Close(player);
        }
    }

    public void Open(PlayerController player)
    {
        isOpened = true;
        chestClosed.SetActive(false);
        chestOpened.SetActive(true);

        MusicManager.Instance.PlayEfx(onOpenAudio);

        if (playerContainerInteractController == null)
        {
            playerContainerInteractController = player.GetComponent<ItemContainerInteractController>();
        }
        playerContainerInteractController.Open(itemContainer, transform);
    }

    public void Close(PlayerController player)
    {
        isOpened = false;
        chestClosed.SetActive(true);
        chestOpened.SetActive(false);

        MusicManager.Instance.PlayEfx(onOpenAudio);

        if (playerContainerInteractController == null)
        {
            playerContainerInteractController = player.GetComponent<ItemContainerInteractController>();
        }
        playerContainerInteractController.Close();
    }
}
