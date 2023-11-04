using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader
{
    public Tag[] tags;
    public float philantropy; //Gut/B�se
    public float knowledge; // F�higheite das Item richtig ein zu sch�tzen
    public float starRating = 2.5F;
    public Trader()
    {
        philantropy = Random.Range(-1.0F, 1.0F);
        knowledge = Random.Range(-1.0F, 1.0F);
        starRating = TradeUpUtility.generateStarRating(philantropy, knowledge);
        int c_tag = < Random.Range(0, 2);
        tags = new Tag[c_tag];
        for (int i = 0; i < c_tag; i++)
        {
            int y = Random.Range(0, TradeUpUtility.allTags.Length - 1);
            tags[i] = TradeUpUtility.allTags[y];
        }
    }

    public float getSubjectiveValue(Item item)
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

        return item.getValue() * intrestModifier;
    }
   
}
