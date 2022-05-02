using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable
{
    [SerializeField] GameObject chestClosed;
    [SerializeField] GameObject chestOpened;
    [SerializeField] bool isOpened;
    [SerializeField] AudioClip onOpenAudio;

    public override void Interact(PlayerController player)
    {
        if (!isOpened)
        {
            isOpened = true;
            chestClosed.SetActive(false);
            chestOpened.SetActive(true);

            MusicManager.Instance.PlayEfx(onOpenAudio);
        }
    }

}
