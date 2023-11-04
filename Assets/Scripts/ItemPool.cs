using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public List<GameObject> itemList = new List<GameObject>();

    public Item GetRandomItemByValue(int value)
    {
        List<GameObject> sammlerItems = new List<GameObject>();
        List<GameObject> neuwertigItems = new List<GameObject>();
        List<GameObject> normalItems = new List<GameObject>();
        List<GameObject> gebrauchtItems = new List<GameObject>();
        List<GameObject> beschädigtItems = new List<GameObject>();

        List<Item> possibleItems = new List<Item>();

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
            if((item.GetComponent<Item>().GetValueByCondition(Condition.Sammler) >= value * 0.9f) && (item.GetComponent<Item>().GetValueByCondition(Condition.Sammler) <= value * 1.1f))
            {
                item.GetComponent<Item>().condition = Condition.Sammler;
                possibleItems.Add(item.GetComponent<Item>());
            }
        }

        foreach (GameObject item in neuwertigItems)
        {
            if ((item.GetComponent<Item>().GetValueByCondition(Condition.Neuwertig) >= value * 0.9f) && (item.GetComponent<Item>().GetValueByCondition(Condition.Neuwertig) <= value * 1.1f))
            {
                item.GetComponent<Item>().condition = Condition.Neuwertig;
                possibleItems.Add(item.GetComponent<Item>());
            }
        }

        foreach (GameObject item in normalItems)
        {
            if ((item.GetComponent<Item>().GetValueByCondition(Condition.Normal) >= value * 0.9f) && (item.GetComponent<Item>().GetValueByCondition(Condition.Normal) <= value * 1.1f))
            {
                item.GetComponent<Item>().condition = Condition.Normal;
                possibleItems.Add(item.GetComponent<Item>());
            }
        }

        foreach (GameObject item in gebrauchtItems)
        {
            if ((item.GetComponent<Item>().GetValueByCondition(Condition.Gebraucht) >= value * 0.9f) && (item.GetComponent<Item>().GetValueByCondition(Condition.Gebraucht) <= value * 1.1f))
            {
                item.GetComponent<Item>().condition = Condition.Gebraucht;
                possibleItems.Add(item.GetComponent<Item>());
            }
        }

        foreach (GameObject item in beschädigtItems)
        {
            if ((item.GetComponent<Item>().GetValueByCondition(Condition.Defekt) >= value * 0.9f) && (item.GetComponent<Item>().GetValueByCondition(Condition.Defekt) <= value * 1.1f))
            {
                item.GetComponent<Item>().condition = Condition.Defekt;
                possibleItems.Add(item.GetComponent<Item>());
            }
        }

        if (possibleItems.Count > 0)
        {
            foreach(Item it in possibleItems)
            {
                Debug.Log("Generated Item: " + it.name + " | Condition: " + it.condition + " | Value: " + it.GetValueByCondition(it.condition));
            }

            return possibleItems[Random.Range(0, possibleItems.Count)];
        }
        else
        {
            Debug.Log("No Item Found For Value: " + value);
            return null;
        }

    }
}
