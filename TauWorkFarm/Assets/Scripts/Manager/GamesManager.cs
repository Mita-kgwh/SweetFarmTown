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
    public DayTimeController dayTimeController;
    public DialogueSystem dialogueSystem;
    public PlaceableObjectReferenceManager placeableObject;
    public ItemList itemDB;
    public OnScreenMessageSystem messageSystem;
    public ScreenTint screenTint;
    public TileMapReadController tileMapReadController;
    [SerializeField] List<string> insideScene;
    [SerializeField] List<string> indarkScene;
    private void Awake()
    {
        if (insideScene.Contains(DataManager.Instance.GetPlayerData().curSceneName))
        {
            dayTimeController.PlayerInside(true);
        }
        else
        {
            //dayTimeController.PlayerInside(false);
            if (indarkScene.Contains(DataManager.Instance.GetPlayerData().curSceneName))
            {
                dayTimeController.PlayerInDark(true);
            }
        }
    }
}
