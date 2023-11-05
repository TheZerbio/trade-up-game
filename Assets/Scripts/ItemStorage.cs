using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStorage
{
    public Tag[] tags = { };
    public Sprite sprite;
    public string name;
    public Condition condition;
    public float baseValue;

    public ItemStorage(string itemName, Condition condition, Sprite sprite, float baseValue, Tag[] tags)
    {
        this.name = itemName;
        this.condition = condition;
        this.sprite = sprite;
        this.baseValue = baseValue;
        this.tags = tags;
    }

    public float GetValueByCondition(Condition condition)
    {
        return baseValue * TradeUpUtility.getConditionModifier(condition);
    }

    public float getValue()
    {
        return baseValue; // * TradeUpUtility.getConditionModifier(condition);
    }
}
