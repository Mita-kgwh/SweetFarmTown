using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodTroughData
{
    public int foodremain;
    public FoodKind kind;

    public FoodTroughData()
    {
        foodremain = 0;
        kind = FoodKind.NONE;
    }
}

public enum FoodKind
{
    NONE,
    Chick,
    Cow
}

public class FoodTrough : Interactable, IPersistant
{
    //[SerializeField] Item food;
    [SerializeField] int capacity;

    [SerializeField] FoodTroughData data;

    private void Start()
    {
        if (data == null)
        {
            data = new FoodTroughData();
        }
    }

    public override void Interact(PlayerController player)
    {
        Slot slot = GamesManager.Instance.dragAndDropController.slot;
        if (slot.item != null)
        {
            Debug.Log(slot.item);
            if (GamesManager.Instance.dragAndDropController.Check(slot.item))
            {
                AddFood(slot.item);
                return;
            }
        }
        

        ToolsBarController toolsBarController = player.GetComponent<ToolsBarController>();
        if (toolsBarController == null) { return; }
        slot = toolsBarController.GetItemSlot;
        if (slot.item == null) { return; }
        if (slot.item.foodKind != FoodKind.NONE)
        {
            if (data.kind == FoodKind.NONE)
            {
                data.kind = slot.item.foodKind;
            }

            if (slot.item.foodKind == data.kind)
            {
                AddFood(slot.item);
                if (slot.item.stackable)
                {
                    slot.Set(slot.item, slot.count - 1);
                }
                else
                {
                    slot.Clear();
                }
                toolsBarController.UpdateCount();
                return;
            }
        }
    }

    public bool CheckFood(int amount)
    {
        return data.foodremain >= amount;
    }

    public void AddFood(Item food)
    {
        if (data.kind == FoodKind.NONE) { data.kind = food.foodKind; }
        if (data.kind != food.foodKind) { return; }
        data.foodremain += food.foodAmount;
        if (data.foodremain > capacity)
        {
            data.foodremain = capacity;
        }
    }

    public void EatFood(int amount)
    {
        data.foodremain -= amount;
    }

    public void Load(string jsonString)
    {
        data = JsonUtility.FromJson<FoodTroughData>(jsonString);
    }

    public string Read()
    {
        return JsonUtility.ToJson(data);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(data.kind.ToString()))
        {
            PetManager petManager = collision.GetComponent<PetManager>();
            if (petManager == null) 
            {
                return; 
            }
            petManager.SetTroughPos(
                new Vector3Int (
                    (int)transform.position.x,
                    (int)transform.position.y,
                    (int)transform.position.z));
        }
    }
}
