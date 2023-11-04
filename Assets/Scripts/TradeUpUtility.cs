using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeUpUtility
{
    public static Tag[] allTags = { Tag.Tech, Tag.Office, Tag.Jewlery, Tag.Jewlery, Tag.Toy, Tag.Utility, Tag.Decoration };
    public static Condition[] allConditions = { Condition.Sammler, Condition.Neuwertig, Condition.Normal, Condition.Gebraucht, Condition.Defekt };

    public static float getConditionModifier(Condition condition)
    {
        switch (condition)
        {
            case Condition.Sammler:
                return 2.0F;
            case Condition.Neuwertig:
                return 1.5F;
            case Condition.Normal:
                return 1.0F;
            case Condition.Gebraucht:
                return 0.75F;
            case Condition.Defekt:
                return 0.5F;
            default:
                return 1.0F;
        }
    }

    public static float generateStarRating(float philantropy, float knowledge)
    {
        float rating = 2.5F;
        rating += philantropy;
        rating += knowledge;
        //Santitze in case of adjustment
        if (rating < 0)
            return 0;
        if (rating > 5)
            return 5;
        return rating;
    }
}
