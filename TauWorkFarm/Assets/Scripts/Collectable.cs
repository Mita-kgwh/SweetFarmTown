using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //public CollectableType type;
    Transform player;

    [SerializeField] float speedPickUp = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float timeToLive = 10f;

    public Item item;
    public int count = 1;

    private void Awake()
    {
        player = GamesManager.Instance.player.transform;
    }

    public void Set(Item _item, int _count)
    {
        item = _item;
        count = _count;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;
    }

    private void Update()
    {
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickUpDistance)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(
            transform.position, 
            player.position, 
            speedPickUp * Time.deltaTime
            );
        if (distance <0.1f)
        {
            //PlayerManager.Instance.inventory.Add(this);
            if (GamesManager.Instance.inventoryContainer != null)
            {
                GamesManager.Instance.inventoryContainer.Add(item, count);
            }
            else
            {
                Debug.Log("No Inventory attached");
            }
            Destroy(this.gameObject);
        }

    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        PlayerManager.Instance.inventory.Add(this);
    //        Destroy(this.gameObject);
    //    }
    //}
    
}
//public enum CollectableType
//{
//    NONE,
//    TOMATO_SEED,
//    TOMATO
//}