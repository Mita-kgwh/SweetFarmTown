using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesManager : MonoBehaviour
{
    private static GamesManager instance;
    public static GamesManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GamesManager>();
            return instance;
        }
    }

    public GameObject player;
    public ItemContainer inventoryContainer;
    public ItemDragAndDropController dragAndDropController;
}
