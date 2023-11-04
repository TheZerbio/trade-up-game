using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Image itemSpriteSlot;
    public Item myCurrentItem;



    public void Start()
    {
        UpdateCurrentItem();

    }

    public void UpdateCurrentItem()
    {
        itemSpriteSlot.sprite = myCurrentItem.sprite;
    }

    public void ChangeCurrentItem(Item newItem)
    {
        myCurrentItem = newItem;

        UpdateCurrentItem();
    }
}
