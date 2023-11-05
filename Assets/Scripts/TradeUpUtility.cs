using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeUpUtility
{
    public static Tag[] allTags = { Tag.Tech, Tag.Office, Tag.Jewlery, Tag.Jewlery, Tag.Toy, Tag.Utility, Tag.Decoration ,Tag.Antiquities};
    public static Condition[] allConditions = { Condition.Sammler, Condition.Neuwertig, Condition.Normal, Condition.Gebraucht, Condition.Defekt };
    public static string[] traderNames = new string[]{
    "FlohmarktProfi77",
    "Schnaeppchenjaeger123",
    "SecondhandQueen",
    "VerkaufsKoenig2023",
    "FundgrubeSammler",
    "Tauschboerse123",
    "GuensitgAngebot123",
    "RetroLiebhaber88",
    "Schnapper007",
    "Verkaufsfee456",
    "SammelLeidenschaft",
    "MarktPlatzMeister",
    "PlauschTauschFuchs",
    "VintageFan12",
    "Handelsgenie45",
    "Angebotssucher89",
    "TroedelHeld777",
    "PlauschTauschKing",
    "BieteUndSuche33",
    "SchnaeppchenSucher22",
    "FlohmarktSchatz",
    "Verkaufsvirtuose",
    "Schnaeppchenjaegerin77",
    "FundgrubeFreak",
    "TauschboerseMeister",
    "SammlerGlueck123",
    "GuensitgVerkauf321",
    "RetroSammlerin99",
    "Schnaeppchenjagd007",
    "Verkaufskoenigin22",
    "SammelLeidenschaftPro",
    "MarktPlatzGuru",
    "TroedelLiebhaber007",
    "PlauschTauschProfi55",
    "VintageLiebhaber44",
    "HandelsGuru66",
    "Angebotssucherin11",
    "PlauschTauschKoenig",
    "BieteUndSuchePro",
    "SchnaeppchenFan112",
    "FlohmarktFee88",
    "VerkaufsFreak2023",
    "FundgrubenQueen",
    "TauschboerseExperte",
    "SammlerGlueck456",
    "PlauschTauschChampion",
    "VintageSchätze77",
    "GuensitgDeal123",
    "RetroFan456",
    "AngebotssucherPro",
    "Handelsgenie22",
    "VerkaufsProfi99",
    "SchnaeppchenLiebhaberin",
    "FlohmarktEntdecker",
    "FundgrubenKoenig",
    "TauschboerseExperte55",
    "SammlerFreude123",
    "PlauschTauschSchatz",
    "Verkaufsmeister123",
    "MarktPlatzHeld33",
    "GuensitgDeal007",
    "RetroSammler55",
    "PlauschTauschPro777",
    "VintageLiebhaberin22",
    "Angebotssucherin44",
    "TroedelHeldin11",
    "Schnaeppchenfuchs99",
    "FlohmarktKoenigin77",
    "VerkaufsVirtuose2023",
    "FundgrubenSchatz",
    "TauschboerseKing123",
    "SammlerGlueck456",
    "PlauschTauschKingpin",
    "GuensitgVerkauf22",
    "RetroLiebhaberin55",
    "Schnaeppchenjaegerin007",
    "VerkaufsQueen22",
    "AngebotssucherExpert",
    "HandelsProfi123",
    "MarktPlatzFreak",
    "TroedelLiebhaberin66",
    "PlauschTauschMeister22",
    "VintageFan777",
    "GuensitgDealPro99",
    "SchnaeppchenSucherin33",
    "FlohmarktFee2023",
    "VerkaufsChampion123",
    "FundgrubenKoenigin55",
    "TauschboerseSpezialist",
    "SammlerLeidenschaftQueen",
    "PlauschTauschSammler22",
    "RetroLiebhaberin99",
    "Angebotssucherin22",
    "TroedelProfi007",
    "SchnaeppchenFan88",
    "FlohmarktSchatz123",
    "VerkaufsExperte22",
    "FundgrubenKoenig88",
    "TauschboerseMeister77",
    "SammlerFreudePro"};
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
        float rating = 0;

        if (knowledge < 0.2f)
            rating += 2;
        else if (knowledge < 0.3f)
            rating += 1;

        if (philantropy > 1.25f)
            rating += 3;
        else if (philantropy > 1.0f)
            rating += 2;
        else if (philantropy > 0.75f)
            rating += 1;

        Debug.Log("Tausche die Grenzen HIER aus, falls minEval, maxEval, minInformed oder maxInformed verändert wurden");

        return rating;
    }

    public static string GetRandomUsername()
    {
        int rand = Random.Range(0, traderNames.Length);
        return traderNames[rand];
    }

    public static string getTagString(Tag tag)
    {
        switch (tag)
        {
            case Tag.Tech:
                return "Technik";
            case Tag.Office:
                return "Büro";
            case Tag.Jewlery:
                return "Schmuck";
            case Tag.Toy:
                return "Spielzeug";
            case Tag.Utility:
                return "Praktisch";
            case Tag.Decoration:
                return "Dekoration";
            case Tag.Antiquities:
                return "Antiquität";
            default:
                return "";
        }
    }

    public static string getConditionString(Condition condition)
    {
        switch (condition)
        {
            case Condition.Sammler:
                return "Sammler";
            case Condition.Neuwertig:
                return "Neuwertig";
            case Condition.Normal:
                return "Normal";
            case Condition.Gebraucht:
                return "Gebraucht";
            case Condition.Defekt:
                return "Beschädigt";
            default:
                return "Unbekannt";
        }
    }
}
