using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeUpUtility
{
    public static Tag[] allTags = { Tag.Tech, Tag.Office, Tag.Jewlery, Tag.Jewlery, Tag.Toy, Tag.Utility, Tag.Decoration ,Tag.Antiquities};
    public static Condition[] allConditions = { Condition.Sammler, Condition.Neuwertig, Condition.Normal, Condition.Gebraucht, Condition.Defekt };
    public static string[] traderNames = new string[]{
    "FlohmarktProfi77",
    "Schn�ppchenjaeger123",
    "SecondhandQueen",
    "VerkaufsK�nig2023",
    "FundgrubeSammler",
    "Tauschb�rse123",
    "G�nstigAngebot123",
    "RetroLiebhaber88",
    "Schnapper007",
    "Verkaufsfee456",
    "SammelLeidenschaft",
    "MarktPlatzMeister",
    "PlauschTauschFuchs",
    "VintageFan12",
    "Handelsgenie45",
    "Angebotssucher89",
    "Tr�delHeld777",
    "PlauschTauschKing",
    "BieteUndSuche33",
    "Schn�ppchenSucher22",
    "FlohmarktSchatz",
    "Verkaufsvirtuose",
    "Schn�ppchenj�gerin77",
    "FundgrubeFreak",
    "Tauschb�rseMeister",
    "SammlerGl�ck123",
    "G�nstigVerkauf321",
    "RetroSammlerin99",
    "Schn�ppchenjagd007",
    "Verkaufsk�nigin22",
    "SammelLeidenschaftPro",
    "MarktPlatzGuru",
    "Tr�delLiebhaber007",
    "PlauschTauschProfi55",
    "VintageLiebhaber44",
    "HandelsGuru66",
    "Angebotssucherin11",
    "PlauschTauschK�nig",
    "BieteUndSuchePro",
    "Schn�ppchenFan112",
    "FlohmarktFee88",
    "VerkaufsFreak2023",
    "FundgrubenQueen",
    "Tauschb�rseExperte",
    "Sammelwut123",
    "PlauschTauschChampion",
    "VintageSch�tze77",
    "G�nstigDeal123",
    "RetroFan456",
    "AngebotssucherPro",
    "Handelsgenie22",
    "VerkaufsProfi99",
    "Schn�ppchenLiebhaberin",
    "FlohmarktEntdecker",
    "FundgrubenK�nig",
    "Tauschb�rseExperte55",
    "SammelFreude123",
    "PlauschTauschSchatz",
    "Verkaufsmeister123",
    "MarktPlatzHeld33",
    "G�nstigDeal007",
    "RetroSammler55",
    "PlauschTauschPro777",
    "VintageLiebhaberin22",
    "Angebotssucherin44",
    "Tr�delHeldin11",
    "Schn�ppchenfuchs99",
    "FlohmarktK�nigin77",
    "VerkaufsVirtuose2023",
    "FundgrubenSchatz",
    "Tauschb�rseKing123",
    "SammelGl�ck456",
    "PlauschTauschKingpin",
    "G�nstigVerkauf22",
    "RetroLiebhaberin55",
    "Schn�ppchenj�gerin007",
    "VerkaufsQueen22",
    "AngebotssucherExpert",
    "HandelsProfi123",
    "MarktPlatzFreak",
    "Tr�delLiebhaberin66",
    "PlauschTauschMeister22",
    "VintageFan777",
    "G�nstigDealPro99",
    "Schn�ppchenSucherin33",
    "FlohmarktFee2023",
    "VerkaufsChampion123",
    "FundgrubenK�nigin55",
    "Tauschb�rseSpezialist",
    "SammelLeidenschaftQueen",
    "PlauschTauschSammler22",
    "RetroLiebhaberin99",
    "Angebotssucherin22",
    "Tr�delProfi007",
    "Schn�ppchenFan88",
    "FlohmarktSchatz123",
    "VerkaufsExperte22",
    "FundgrubenK�nig88",
    "Tauschb�rseMeister77",
    "SammelFreudePro"};
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

        Debug.Log("Tausche die Grenzen HIER aus, falls minEval, maxEval, minInformed oder maxInformed ver�ndert wurden");

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
                return "Buero";
            case Tag.Jewlery:
                return "Schmuck";
            case Tag.Toy:
                return "Spielzeug";
            case Tag.Utility:
                return "Praktische Sachen";
            case Tag.Decoration:
                return "Dekorationen";
            case Tag.Antiquities:
                return "Antiquitaeten";
            default:
                return "Unknown Tag";
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
                return "Defekt";
            default:
                return "Unbekannt";
        }
    }
}
