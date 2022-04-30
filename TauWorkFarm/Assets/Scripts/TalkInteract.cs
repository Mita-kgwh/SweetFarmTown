using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteract : Interactable
{
    [SerializeField] DialogueContainer dialogue;
    public override void Interact(PlayerController player)
    {
        GamesManager.Instance.dialogueSystem.Initialize(dialogue);
    }
}
