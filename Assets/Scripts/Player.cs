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
    public Button acceptTradeButton;
    public Button rejectTradeButton;
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

        // Mache nur einen Call, wenn man mehr als 1 Angebot in der Warteschlange hat
        if(numberOfTraders > 1)
            StartCoroutine(ShowCallAfterDelay(1, Random.Range(delayForNextCall - delayForNextCall_Offset, delayForNextCall + delayForNextCall_Offset)));

        traderPrefabList[currentTraderIndex].SetActive(true);

        // Wenn man nur 1 Angebot hat, dann deaktivier reject-button --> Man muss das Angebot annehmen
        if (numberOfTraders == 1)
            rejectTradeButton.interactable = false;
    }

    public void ShowNewOffer()
    {
        StopAllCoroutines();
        callWindow.SetActive(false);
        traderPrefabList[currentTraderIndex].SetActive(false);
        currentTraderIndex++;
        traderPrefabList[currentTraderIndex].SetActive(true);
        if (currentTraderIndex + 1 >= traderPrefabList.Count) 
        {
            rejectTradeButton.interactable = false;
        }
        else
            StartCoroutine(ShowCallAfterDelay(currentTraderIndex + 1, Random.Range(delayForNextCall - delayForNextCall_Offset, delayForNextCall + delayForNextCall_Offset)));
    }

    public void RejectCall()
    {
        StopAllCoroutines();
        callWindow.SetActive(false);
        Debug.Log("Play New Voice Line");
        StartCoroutine(ShowCallAfterDelay(currentTraderIndex + 1, Random.Range(delayForNextCall - delayForNextCall_Offset, delayForNextCall + delayForNextCall_Offset)));
    }

    public void AcceptTrade()
    {
        StopAllCoroutines();
        myCurrentItem = traderPrefabList[currentTraderIndex].GetComponent<Trader>().traderItem;
        UpdateCurrentItem();

        foreach(GameObject trader in traderPrefabList)
        {
            Destroy(trader);
        }

        traderPrefabList.Clear();

        currentTraderIndex = 0;

        GenerateTraderList();
    }

    public void RejectTrade()
    {       
        ShowNewOffer();
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
