using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Call Settings")]
    public GameObject callWindow;

    [Header("Trader Settings")]
    public float delayForNextCall_Offset;
    public float delayForNextCall;
    public float callDuration;
    public int minTraderForCurrentItem;
    public int maxTraderForCurrentItem;
    public GameObject traderPrefab;
    public Transform traderPrefabParent;
    private List<GameObject> traderPrefabList = new List<GameObject>();
    private int numberOfTraders;

    [Header("Player Item")]
    public Image itemSpriteSlot;
    public Item myCurrentItem;


    private int currentTraderIndex = 0;


    public void Start()
    {
        UpdateCurrentItem();
        GenerateTraderList();
    }

    public void GenerateTraderList()
    {
        numberOfTraders = Random.Range(minTraderForCurrentItem, maxTraderForCurrentItem + 1);

        for(int i = 0; i < numberOfTraders; i++)
        {
            GameObject trader = Instantiate(traderPrefab, traderPrefabParent);
            trader.SetActive(false);
            traderPrefabList.Add(trader); 
        }

        Debug.Log("Zeig nur den 1. Trader in der UI an");
        StartCoroutine(ShowCallAfterDelay(1, Random.Range(delayForNextCall - delayForNextCall_Offset, delayForNextCall + delayForNextCall_Offset)));

        traderPrefabList[currentTraderIndex].SetActive(true);
    }

    public void ShowNewOffer()
    {
        StopAllCoroutines();
        callWindow.SetActive(false);
        traderPrefabList[currentTraderIndex].SetActive(false);
        currentTraderIndex++;
        traderPrefabList[currentTraderIndex].SetActive(true);
        StartCoroutine(ShowCallAfterDelay(currentTraderIndex + 1, Random.Range(delayForNextCall - delayForNextCall_Offset, delayForNextCall + delayForNextCall_Offset)));
    }

    public void RejectCall()
    {
        StopAllCoroutines();
        Debug.Log("Play New Voice Line");
        StartCoroutine(ShowCallAfterDelay(currentTraderIndex + 1, Random.Range(delayForNextCall - delayForNextCall_Offset, delayForNextCall + delayForNextCall_Offset)));
    }

    private IEnumerator ShowCallAfterDelay(int traderIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Show Call");

        callWindow.transform.GetChild(0).GetComponent<Image>().sprite = traderPrefabList[traderIndex].GetComponent<Trader>().profilePicture;
        callWindow.transform.GetChild(1).GetComponent<TMP_Text>().text = traderPrefabList[traderIndex].GetComponent<Trader>().traderName;
        callWindow.SetActive(true);

        yield return new WaitForSeconds(callDuration);

        callWindow.SetActive(false);

        if ((traderIndex + 1) < traderPrefabList.Count)
            StartCoroutine(ShowCallAfterDelay(traderIndex + 1, delayForNextCall));
        else
            Debug.Log("No More Offers/Traders");
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
