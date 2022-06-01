using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Data/Reward Actions/Unlocked New Item")]
public class UnlockedNewItem : RewardActions
{
    [SerializeField] ItemContainer itemContainer;
    [SerializeField] List<Item> items;
    [SerializeField] int amount;
    [SerializeField] string descriptionText;

    public override void UnlockedNewItemApply()
    {
        QuestManager.Instance.ShowRewards();
        for (int i = 0; i < items.Count; i++)
        {
            itemContainer.Add(items[i]);
            QuestManager.Instance.GetRewardsPanel().SetSprite(items[i].icon).SetAmount(0);
        }
    }

    public override void MoneyReward()
    {
        QuestManager.Instance.GetRewardsPanel().SetDescription(descriptionText + " va phan tien mat tri gia " + amount.ToString());
        PlayerManager.Instance.AddCurrency(amount);
    }
}
