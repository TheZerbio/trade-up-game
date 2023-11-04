using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public VoiceClipManager voiceClipManager;
    public TMP_Text numberOfOffers_Text;

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
    private bool activeOffer = false;

    [Header("Player Item")]
    public Image itemSpriteSlot;
    public Item myCurrentItem;


    private int currentTraderIndex = 0;
    private int currentCallIndex = 0;


    public void Start()
    {
        UpdateCurrentItem();
        GenerateTraderList();
    }

    public void GenerateTraderList()
    {
        numberOfTraders = Random.Range(minTraderForCurrentItem, maxTraderForCurrentItem + 1);
        numberOfOffers_Text.text = "Interessenten f�r dein Produkt: " + numberOfTraders;

        for (int i = 0; i < numberOfTraders; i++)
        {
            GameObject trader = Instantiate(traderPrefab, traderPrefabParent);
            trader.SetActive(false);
            traderPrefabList.Add(trader); 
        }

        //Debug.Log("Zeig nur den 1. Trader in der UI an");

        // Mache nur einen Call, wenn man mehr als 1 Angebot in der Warteschlange hat
        if(numberOfTraders > 1)
            StartCoroutine(ShowCallAfterDelay(3));

        //traderPrefabList[currentTraderIndex].SetActive(true);

        // Wenn man nur 1 Angebot hat, dann deaktivier reject-button --> Man muss das Angebot annehmen
        //if (numberOfTraders == 1)
            //rejectTradeButton.interactable = false;
    }

    public void ShowNewOffer()
    {
        StopAllCoroutines();

        callWindow.SetActive(false);

        currentTraderIndex = currentCallIndex;

        Trader activeTrader = traderPrefabList[currentTraderIndex].GetComponent<Trader>();

        // play random opening line
        voiceClipManager.PlayVoiceLine(activeTrader);
        // disable opening lines from same trader
        activeTrader.firstCall = false;

        if (!activeOffer)
        {
            acceptTradeButton.gameObject.SetActive(true);
            rejectTradeButton.gameObject.SetActive(true);

            traderPrefabList[currentTraderIndex].SetActive(true);
            activeOffer = true;
        }
        else
        {
            foreach(GameObject trader in traderPrefabList)
            {
                trader.SetActive(false);
            }
            traderPrefabList[currentTraderIndex].SetActive(true);
        }

        if (currentTraderIndex == traderPrefabList.Count - 1)
        {
            rejectTradeButton.interactable = false;
        }
        else
        {
            currentCallIndex++;
            StartCoroutine(ShowCallAfterDelay(Random.Range(delayForNextCall - delayForNextCall_Offset, delayForNextCall + delayForNextCall_Offset)));
        }
    }

    public void RejectCall()
    {
        StopAllCoroutines();
        callWindow.SetActive(false);

        // play secondary voice lines of active trader
        Debug.Log("Play Next Line");
        voiceClipManager.PlayVoiceLine(traderPrefabList[currentTraderIndex].GetComponent<Trader>());


        currentCallIndex++;

        // Wenn ich den letzten call gerade abgelehnt habe, dann disable reject button
        if (currentCallIndex >= traderPrefabList.Count)
        {
            rejectTradeButton.interactable = false;
            return;
        }

        StartCoroutine(ShowCallAfterDelay(Random.Range(delayForNextCall - delayForNextCall_Offset, delayForNextCall + delayForNextCall_Offset)));
    }

    public void AcceptTrade()
    {
        StopAllCoroutines();
        myCurrentItem = traderPrefabList[currentTraderIndex].GetComponent<Trader>().traderItem;

        activeOffer = false;

        UpdateCurrentItem();

        foreach(GameObject trader in traderPrefabList)
        {
            Destroy(trader);
        }

        traderPrefabList.Clear();

        currentTraderIndex = 0;
        currentCallIndex = 0;

        acceptTradeButton.gameObject.SetActive(false);
        rejectTradeButton.interactable = true;
        rejectTradeButton.gameObject.SetActive(false);
        callWindow.transform.GetChild(3).GetComponent<Button>().interactable = true;

        GenerateTraderList();
    }

    public void RejectTrade()
    {
        activeOffer = false;

        acceptTradeButton.gameObject.SetActive(false);
        rejectTradeButton.gameObject.SetActive(false);

        traderPrefabList[currentTraderIndex].SetActive(false);
        ShowCallAfterDelay(3);
    }

    private IEnumerator ShowCallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Show Call");
        bool lastCall = false;

        if ((currentCallIndex + 1) >= traderPrefabList.Count && !activeOffer)
        {
            Debug.Log("Last Call");
            lastCall = true;
            callWindow.transform.GetChild(3).GetComponent<Button>().interactable = false;
        }

        callWindow.transform.GetChild(0).GetComponent<Image>().sprite = traderPrefabList[currentCallIndex].GetComponent<Trader>().profilePicture;
        callWindow.transform.GetChild(1).GetComponent<TMP_Text>().text = traderPrefabList[currentCallIndex].GetComponent<Trader>().traderName;
        callWindow.SetActive(true);

        yield return new WaitForSeconds(callDuration);

        if (lastCall)
        {
            yield break;
        }

        Debug.Log("Test");

        callWindow.SetActive(false);

        if ((currentCallIndex + 1) < traderPrefabList.Count)
        {
            currentCallIndex++;
            StartCoroutine(ShowCallAfterDelay(delayForNextCall));
        }
        else
        {
            rejectTradeButton.interactable = false;
            Debug.Log("No More Offers/Traders");
        }
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
