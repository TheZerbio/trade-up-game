using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public VoiceClipManager voiceClipManager;
    public Text numberOfOffers_Text;
    public Canvas gameoverCanvas;

    [Header("Call Settings")]
    public GameObject callWindow;
    public float delayForNextCall_Offset;
    public float delayForNextCall;
    public float callDuration;

    [Header("Trader Settings")]
    public Button acceptTradeButton;
    public Button rejectTradeButton;
    private int minTraderForCurrentItem;
    private int maxTraderForCurrentItem;
    public GameObject traderPrefab;
    public Transform traderPrefabParent;   
    private int numberOfTraders; 

    [Header("Player Item")]
    public Image itemSpriteSlot;
    public Text itemNameText;
    public Text itemConditionText;
    public Text itemTagsText;
    public Item startItem;
    public GameObject targetItem;

    [Header("Debug Variables (Don't Set!!)")]
    public List<GameObject> traderPrefabList = new List<GameObject>();
    public ItemStorage myCurrentItem;
    public int currentCallIndex = 0;
    public bool activeOffer = false;
    public bool tutorialCall = true;

    private int currentTraderIndex = 0;
    private int remaining0ffers = 0;
    


    public void Start()
    {
        myCurrentItem = new ItemStorage(startItem.name, Condition.Normal, startItem.GetComponentInChildren<SpriteRenderer>().sprite, startItem.baseValue, startItem.tags);
        UpdateCurrentItem();

        StartCoroutine(ShowTutorialCall());

        //GenerateTraderList(true);
    }

    public IEnumerator ShowTutorialCall()
    {
        yield return new WaitForSeconds(3);

        callWindow.transform.GetChild(1).GetComponent<Text>().text = "Kundendienst";
        callWindow.SetActive(true);
    }

    public void GenerateTraderList(bool firstItem)
    {
        numberOfTraders = GetNumberOfTradersBasedOnCurrentItemCondition(firstItem);
        remaining0ffers = numberOfTraders;
        numberOfOffers_Text.text = "Interessenten: " + remaining0ffers;

        for (int i = 0; i < numberOfTraders; i++)
        {
            GameObject trader = Instantiate(traderPrefab, traderPrefabParent);
            trader.GetComponent<Trader>().InitTrader();
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

    public IEnumerator Delay(float tutorialDelay)
    {
        yield return new WaitForSeconds(tutorialDelay);
        tutorialCall = false;
        callWindow.transform.GetChild(2).GetComponent<Button>().interactable = true;
        GenerateTraderList(true);
    }

    public void ShowNewOffer()
    {
        if (tutorialCall)
        {          
            callWindow.transform.GetChild(2).GetComponent<Button>().interactable = false;
            callWindow.GetComponent<Animator>().enabled = false;
            Debug.Log("Play Tutorial Voice Line");
            StartCoroutine(Delay(90));
        }
        else
        {
            StopAllCoroutines();

        remaining0ffers--;
        numberOfOffers_Text.text = "Interessenten: " + remaining0ffers;

            callWindow.SetActive(false);

            currentTraderIndex = currentCallIndex;

        Trader activeTrader = traderPrefabList[currentTraderIndex].GetComponent<Trader>();

        // play random opening line
        voiceClipManager.PlayVoiceLine(activeTrader);
        // disable opening lines from same trader
        activeTrader.stage++;

        if (!activeOffer)
        {
            acceptTradeButton.gameObject.SetActive(true);
            rejectTradeButton.gameObject.SetActive(true);

                traderPrefabList[currentTraderIndex].SetActive(true);
                activeOffer = true;
            }
            else
            {
                foreach (GameObject trader in traderPrefabList)
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
    }

    public void RejectCall()
    {
        if (tutorialCall)
        {
            StopAllCoroutines();
            callWindow.transform.GetChild(2).GetComponent<Button>().interactable = true;
            tutorialCall = false;
            callWindow.SetActive(false);
            GenerateTraderList(true);
        }
        else
        {
            StopAllCoroutines();
            callWindow.SetActive(false);
            Debug.Log("Play New Voice Line");

            Trader activeTrader = traderPrefabList[currentTraderIndex].GetComponent<Trader>();

            // play secondary voice lines of active trader
            voiceClipManager.PlayVoiceLine(activeTrader);
            // increase number of voicelines played by same trader
            activeTrader.stage++;

            traderPrefabList[currentTraderIndex].GetComponent<Trader>().addInterestTag();
            currentCallIndex++;

            remaining0ffers--;
            numberOfOffers_Text.text = "Interessenten: " + remaining0ffers;

            // Wenn ich den letzten call gerade abgelehnt habe, dann disable reject button
            if (currentCallIndex >= traderPrefabList.Count)
            {
                rejectTradeButton.interactable = false;
                return;
            }

            StartCoroutine(ShowCallAfterDelay(Random.Range(delayForNextCall - delayForNextCall_Offset, delayForNextCall + delayForNextCall_Offset)));
        }
    }

    public void AcceptTrade()
    {
        StopAllCoroutines();
        myCurrentItem = traderPrefabList[currentTraderIndex].GetComponent<Trader>().itemStorage;

        Debug.Log("My Current Item Condition: " + myCurrentItem.condition + " | Current Trader Index: " + currentTraderIndex);

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

        bool winning = CheckWinningCondition();

        if (!winning)
            GenerateTraderList(false);
        else
        {
            numberOfOffers_Text.text = "Du hast gewonnen!";           
            StartCoroutine(DisplayWinningScreen());
        }
    }

    private IEnumerator DisplayWinningScreen()
    {
        yield return new WaitForSeconds(3);
        gameoverCanvas.gameObject.SetActive(true);
    }

    public bool CheckWinningCondition()
    {
        if(myCurrentItem.name == targetItem.name)
            return true;
        return false;
    }

    public void RejectTrade()
    {
        activeOffer = false;

        acceptTradeButton.gameObject.SetActive(false);
        rejectTradeButton.gameObject.SetActive(false);

        traderPrefabList[currentTraderIndex].SetActive(false);
        StartCoroutine(ShowCallAfterDelay(3));
    }

    private IEnumerator ShowCallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Trader activeTrader = traderPrefabList[currentTraderIndex].GetComponent<Trader>();

        bool lastCall = false;

        if ((currentCallIndex + 1) >= traderPrefabList.Count && !activeOffer)
        {
            //Debug.Log("Last Call");
            lastCall = true;
            callWindow.transform.GetChild(3).GetComponent<Button>().interactable = false;
        }

        //callWindow.transform.GetChild(0).GetComponent<Image>().sprite = traderPrefabList[currentCallIndex].GetComponent<Trader>().profilePicture;
        callWindow.transform.GetChild(1).GetComponent<Text>().text = traderPrefabList[currentCallIndex].GetComponent<Trader>().traderName;
        callWindow.SetActive(true);

        yield return new WaitForSeconds(callDuration);

        remaining0ffers--;
        numberOfOffers_Text.text = "Interessenten: " + remaining0ffers;

        traderPrefabList[currentTraderIndex].GetComponent<Trader>().addInterestTag();

        // play next voice-line of active trader if there is an active trader
        if (activeOffer)
        {
            voiceClipManager.PlayVoiceLine(activeTrader);
            activeTrader.stage++;
        }

        if (lastCall)
        {
            yield break;
        }

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
        itemSpriteSlot.sprite = myCurrentItem.sprite; //.GetComponentInChildren<SpriteRenderer>().sprite;
        itemNameText.text = myCurrentItem.name;
        itemConditionText.text = TradeUpUtility.getConditionString(myCurrentItem.condition);

        foreach(Tag t in myCurrentItem.tags)
        {
            itemTagsText.text = TradeUpUtility.getTagString(t) + "\n";
        }      
    }

    public void ChangeCurrentItem(ItemStorage newItem)
    {
        myCurrentItem = newItem;

        UpdateCurrentItem();
    }

    public int GetNumberOfTradersBasedOnCurrentItemCondition(bool firstItem)
    {
        if (!firstItem)
        {
            switch (myCurrentItem.condition)
            {
                case Condition.Sammler: return Random.Range(6, 9);
                case Condition.Neuwertig: return Random.Range(5, 8);
                case Condition.Normal: return Random.Range(4, 7);
                case Condition.Gebraucht: return Random.Range(3, 6);
                case Condition.Defekt: return Random.Range(2, 5);
                default: return Random.Range(2, 9);
            }
        }
        else
        {
            return Random.Range(4, 7);
        }
    }
}
