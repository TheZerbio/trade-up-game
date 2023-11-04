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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getValue()
    {
        return baseValue * TradeUpUtility.getConditionModifier(condition);
    }
}
