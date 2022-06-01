using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsPanel : MonoBehaviour
{
    [SerializeField] Image iconReward;
    [SerializeField] Text questDescriptionText;
    [SerializeField] Text amount;
    [SerializeField] AudioClip rewardEffect;

    private void OnEnable()
    {
        MusicManager.Instance.PlayEfx(rewardEffect);
    }

    public RewardsPanel SetSprite(Sprite sprite)
    {
        iconReward.sprite = sprite;
        return this;
    }

    public RewardsPanel SetAmount(int _amount)
    {
        amount.text = "";
        if (_amount != 0)
        {
            amount.text = _amount.ToString();
        } 
        return this;
    }

    public RewardsPanel SetDescription(string description)
    {
        questDescriptionText.text = description;
        return this;
    }
}
