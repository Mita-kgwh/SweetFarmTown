using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartCowMission : MonoBehaviour
{
    [SerializeField] string collectTag;
    [SerializeField] Animator animator;
    [SerializeField] DialogueContainer dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(collectTag))
        {
            Destroy(collision.gameObject);
            animator.SetTrigger("CanGo");
            GamesManager.Instance.dialogueSystem.Initialize(dialogue);
        }
    }
}
