using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Tag[] tags = { };
    [Range(0.0F, 100.0F)]
    public float baseValue = 10;
    public Sprite sprite = null;
    public Condition condition = Condition.Normal;


    public float GetValueByCondition(Condition condition)
    {
        return baseValue * TradeUpUtility.getConditionModifier(condition);
    }

    public float getValue()
    {
        return baseValue * TradeUpUtility.getConditionModifier(condition);
    }
}
