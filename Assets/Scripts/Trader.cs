using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trader : MonoBehaviour
{
    public Tag[] tags;
    public float philantropy; //Gut/Böse = eval
    public float knowledge; // Fähigheite das Item richtig ein zu schätzen = informed
    public float starRating = 2.5F;
    public string traderName = "DefaultName";
    public int voiceType;           // which voice should be played?
    public int stage;               // how many voiceLines have been called already?
    public Sprite profilePicture;
    public Image tradeItemImageSlot;
    //public Item traderItem;
    public ItemStorage itemStorage;
    public AudioSource audioSource;

    private Text interests;
    private Text zustand;
    private int c_tag;
    private int cTag = 0;

    public void InitTrader()
    {
        c_tag = Random.Range(0, 4);
        tags = new Tag[c_tag];
        for (int i = 0; i < c_tag; i++)
        {
            int y = Random.Range(0, TradeUpUtility.allTags.Length);
            tags[i] = TradeUpUtility.allTags[y];
            //Debug.Log("added a Tag");
        }

        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ItemPool itemPool = GameObject.FindGameObjectWithTag("ItemPool").GetComponent<ItemPool>();

        //Initialise Local Variables
        audioSource = gameObject.GetComponent<AudioSource>();
        philantropy = Random.Range(0.5f, 1.5f);         // philantropy = Random.Range(-1.0F, 1.0F);
        knowledge = Random.Range(0.1f, 0.4f);           // knowledge = Random.Range(-1.0F, 1.0F);
        traderName = TradeUpUtility.GetRandomUsername();
        starRating = TradeUpUtility.generateStarRating(philantropy, knowledge);
        stage = 0;                      // to get the intro voiceline first
        voiceType = Random.Range(1, 3);     // random person voice

        //Debug.Log("Player Item Base Value: " + player.myCurrentItem.baseValue);
        //Debug.Log("Player Item Subjective Value: " + getSubjectiveValue(player.myCurrentItem));

        int traderItemValue = RandomOfferGenerator.GenerateItemValue(getSubjectiveValue(player.myCurrentItem), philantropy, knowledge);

        itemStorage = itemPool.GetRandomItemByValue(traderItemValue, 0.9f, 1.1f);

        //traderItem = itemPool.GetRandomItemByValue(traderItemValue, 0.9f, 1.1f);

        Debug.Log("TraderItem Name: " + itemStorage.name + " | Condition: " + itemStorage.condition);

        if (itemStorage != null)
            tradeItemImageSlot.sprite = itemStorage.sprite; //.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;

        
        var Texts = gameObject.GetComponentsInChildren<Text>();
        for(int i = 0; i < Texts.Length; i++)
        {
            if (Texts[i].gameObject.name.Equals("Ranking"))
            {
                Texts[i].text = "" + starRating + "/5";
            }
            if (Texts[i].gameObject.name.Equals("NamensSchild"))
            {
                Texts[i].text = traderName;
            }
            if (Texts[i].gameObject.name.Equals("Interessen"))
            {
                interests = Texts[i];
            }
            if (Texts[i].gameObject.name.Equals("Zustand"))
            {
                zustand = Texts[i];
                zustand.text = TradeUpUtility.getConditionString(itemStorage.condition);
            }
        }
    }

    public void addInterestTag()
    {
        if (cTag < c_tag)
        {
            interests.text += TradeUpUtility.getTagString(tags[cTag])+"  ";
            cTag++;
        }
    }

    public int getSubjectiveValue(ItemStorage item)
    {
        float intrestModifier = 1.0F;
        for (int i = 0; i < tags.Length; i++)
        {
            for(int j = 0; j < item.tags.Length; j++)
            {
                if (tags[i] == item.tags[j])
                {
                    intrestModifier += 0.1F;
                }
            }
        }

        return (int) (item.getValue() * intrestModifier);
    }

    public void print()
    {
        Debug.Log(traderName);
        Debug.Log("" + philantropy + "   " + knowledge);
        Debug.Log("StarRating: " + starRating);
        Debug.Log(tags);
        Debug.Log("------------------");
    }
   
}
