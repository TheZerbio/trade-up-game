using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public List<GameObject> itemList = new List<GameObject>();
    public GameObject tophat;

    public ItemStorage GetRandomItemByValue(float value, float minRange, float maxRange)
    {
        List<GameObject> sammlerItems = new List<GameObject>();
        List<GameObject> neuwertigItems = new List<GameObject>();
        List<GameObject> normalItems = new List<GameObject>();
        List<GameObject> gebrauchtItems = new List<GameObject>();
        List<GameObject> beschädigtItems = new List<GameObject>();

        List<ItemStorage> possibleItems = new List<ItemStorage>();

        foreach (GameObject item in itemList)
        {
            sammlerItems.Add(item);
            neuwertigItems.Add(item);
            normalItems.Add(item);
            gebrauchtItems.Add(item);
            beschädigtItems.Add(item);
        }

        foreach (GameObject item in sammlerItems)
        {
            if((item.GetComponent<Item>().GetValueByCondition(Condition.Sammler) >= value * minRange) && (item.GetComponent<Item>().GetValueByCondition(Condition.Sammler) <= value * maxRange))
            {
                //item.GetComponent<Item>().condition = Condition.Sammler;
                ItemStorage storage = new ItemStorage(item.GetComponent<Item>().name, Condition.Sammler, item.GetComponentInChildren<SpriteRenderer>().sprite, item.GetComponent<Item>().baseValue, item.GetComponent<Item>().tags);
                possibleItems.Add(storage);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            foreach (GameObject item in neuwertigItems)
            {
                if ((item.GetComponent<Item>().GetValueByCondition(Condition.Neuwertig) >= value * minRange) && (item.GetComponent<Item>().GetValueByCondition(Condition.Neuwertig) <= value * maxRange))
                {
                    //item.GetComponent<Item>().condition = Condition.Neuwertig;
                    ItemStorage storage = new ItemStorage(item.GetComponent<Item>().name, Condition.Neuwertig, item.GetComponentInChildren<SpriteRenderer>().sprite, item.GetComponent<Item>().baseValue, item.GetComponent<Item>().tags);
                    possibleItems.Add(storage);
                }
            }
        }

        for(int i = 0; i < 4; i++)
        {
            foreach (GameObject item in normalItems)
            {
                if ((item.GetComponent<Item>().GetValueByCondition(Condition.Normal) >= value * minRange) && (item.GetComponent<Item>().GetValueByCondition(Condition.Normal) <= value * maxRange))
                {
                    //item.GetComponent<Item>().condition = Condition.Normal;
                    ItemStorage storage = new ItemStorage(item.GetComponent<Item>().name, Condition.Normal, item.GetComponentInChildren<SpriteRenderer>().sprite, item.GetComponent<Item>().baseValue, item.GetComponent<Item>().tags);
                    possibleItems.Add(storage);
                }
            }
        }

        for (int i = 0; i < 2; i++)
        {
            foreach (GameObject item in gebrauchtItems)
            {
                if ((item.GetComponent<Item>().GetValueByCondition(Condition.Gebraucht) >= value * minRange) && (item.GetComponent<Item>().GetValueByCondition(Condition.Gebraucht) <= value * maxRange))
                {
                    //item.GetComponent<Item>().condition = Condition.Gebraucht;
                    ItemStorage storage = new ItemStorage(item.GetComponent<Item>().name, Condition.Gebraucht, item.GetComponentInChildren<SpriteRenderer>().sprite, item.GetComponent<Item>().baseValue, item.GetComponent<Item>().tags);
                    possibleItems.Add(storage);
                }
            }
        }

        for (int i = 0; i < 2; i++)
        {
            foreach (GameObject item in beschädigtItems)
            {
                if ((item.GetComponent<Item>().GetValueByCondition(Condition.Defekt) >= value * minRange) && (item.GetComponent<Item>().GetValueByCondition(Condition.Defekt) <= value * maxRange))
                {
                    //item.GetComponent<Item>().condition = Condition.Defekt;
                    ItemStorage storage = new ItemStorage(item.GetComponent<Item>().name, Condition.Defekt, item.GetComponentInChildren<SpriteRenderer>().sprite, item.GetComponent<Item>().baseValue, item.GetComponent<Item>().tags);
                    possibleItems.Add(storage);
                }
            }
        }

        if (possibleItems.Count > 0)
        {
            /**
            foreach(Item it in possibleItems)
            {
                Debug.Log("Generated Item: " + it.name + " | Condition: " + it.condition + " | Value: " + it.GetValueByCondition(it.condition));
            }
            **/

            ItemStorage item = possibleItems[Random.Range(0, possibleItems.Count)];

            if(item.name == tophat.name)
                item.condition = Condition.Normal;

            return item;
        }
        else
        {          
            Debug.Log("No Item Found For Value: " + value);
            return GetRandomItemByValue(value, minRange - 0.05f, maxRange + 0.05f);
        }
    }
}
