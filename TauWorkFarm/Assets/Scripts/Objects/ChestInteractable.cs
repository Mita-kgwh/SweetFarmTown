using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteractable : Interactable, IPersistant
{
    [SerializeField] GameObject chestClosed;
    [SerializeField] GameObject chestOpened;
    [SerializeField] bool isOpened;
    [SerializeField] AudioClip onOpenAudio;
    [SerializeField] ItemContainer itemContainer;
    ItemContainerInteractController playerContainerInteractController;

    private void Start()
    {
        if (itemContainer == null)
        {
            Init();
        }
    }

    private void Init()
    {
        itemContainer = (ItemContainer)ScriptableObject.CreateInstance(typeof(ItemContainer));
        itemContainer.Init();
    }

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

    [System.Serializable]
    public class SaveLootItemData
    {
        public int itemId;
        public int count;

        public SaveLootItemData(int id, int _count)
        {
            itemId = id;
            count = _count;
        }
    }

    [System.Serializable]
    public class ToSave
    {
        public List<SaveLootItemData> itemDatas;

        public ToSave()
        {
            itemDatas = new List<SaveLootItemData>();
        }
    }

    public string Read()
    {
        ToSave toSave = new ToSave();

        for (int i = 0; i < itemContainer.slots.Count; i++)
        {
            if (itemContainer.slots[i].item == null)
            {
                toSave.itemDatas.Add(new SaveLootItemData(-1, 0));
            }
            else
            {
                toSave.itemDatas.Add(new SaveLootItemData(
                    itemContainer.slots[i].item.id, 
                    itemContainer.slots[i].count));
            }
        }

        return JsonUtility.ToJson(toSave);
    }

    public void Load(string jsonString)
    {
        if (jsonString == "" || jsonString == "{}") { return; }
        if (itemContainer == null)
        {
            Init();
        }
        ToSave toLoad = JsonUtility.FromJson<ToSave>(jsonString);
        for (int i = 0; i < toLoad.itemDatas.Count; i++)
        {
            if (toLoad.itemDatas[i].itemId == -1)
            {
                itemContainer.slots[i].Clear();
            }
            else
            {
                itemContainer.slots[i].item = GamesManager.Instance.itemDB.items[toLoad.itemDatas[i].itemId];
                itemContainer.slots[i].count = toLoad.itemDatas[i].count;
            }
        }

    }
}
