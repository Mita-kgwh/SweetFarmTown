using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Reward Actions/Unlocked New Item")]
public class UnlockedNewItem : RewardActions
{
    [SerializeField] ItemContainer itemContainer;
    [SerializeField] List<Item> items;
    [SerializeField] int amount;

    public override void UnlockedNewItemApply()
    {
        for (int i = 0; i < items.Count; i++)
        {
            itemContainer.Add(items[i]);
        }
    }

    public override void MoneyReward()
    {
        PlayerManager.Instance.AddCurrency(amount);
    }
}
