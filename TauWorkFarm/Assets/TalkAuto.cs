using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkAuto : MonoBehaviour
{
    [SerializeField] DialogueContainer dialogue;
    Transform player;
    [SerializeField] float speed;
    bool detected;
    bool donetalk;
    public float talkDistance = 1f;

    private void Update()
    {
        if (detected)
        {
            transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
            );
            if (CheckDistance())
            {
                detected = false;
                StartMission();
            }
        }
        
        if (GamesManager.Instance.dialogueSystem.doneTalk)
        {
            Destroy(gameObject);
        }

    }

    private bool CheckDistance()
    {
        return Vector2.Distance(transform.position, player.position) < talkDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            detected = true;
            DisableControls.Instance.DisableControl();
            player = collision.transform;
        }
    }

    private void StartMission()
    {
        GamesManager.Instance.dialogueSystem.Initialize(dialogue);
    }
}
