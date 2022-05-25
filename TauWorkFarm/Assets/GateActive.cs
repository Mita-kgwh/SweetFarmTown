using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateActive : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GamesManager.Instance.tileMapReadController.groundManager.OpenGate(transform.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GamesManager.Instance.tileMapReadController.groundManager.CloseGate(transform.position);
        }   
    }
}
