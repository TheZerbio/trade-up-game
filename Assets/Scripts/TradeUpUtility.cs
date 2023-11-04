using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeUpUtility
{
    public static Tag[] allTags = { Tag.Tech, Tag.Office, Tag.Jewlery, Tag.Jewlery, Tag.Toy, Tag.Utility, Tag.Decoration };
    public static Condition[] allConditions = { Condition.Sammler, Condition.Neuwertig, Condition.Normal, Condition.Gebraucht, Condition.Defekt };
    public static string[] traderNames = new string[]{
    "FlohmarktProfi77",
    "Schnäppchenjaeger123",
    "SecondhandQueen",
    "VerkaufsKönig2023",
    "FundgrubeSammler",
    "Tauschbörse123",
    "GünstigAngebot123",
    "RetroLiebhaber88",
    "Schnapper007",
    "Verkaufsfee456",
    "SammelLeidenschaft",
    "MarktPlatzMeister",
    "PlauschTauschFuchs",
    "VintageFan12",
    "Handelsgenie45",
    "Angebotssucher89",
    "TrödelHeld777",
    "PlauschTauschKing",
    "BieteUndSuche33",
    "SchnäppchenSucher22",
    "FlohmarktSchatz",
    "Verkaufsvirtuose",
    "Schnäppchenjägerin77",
    "FundgrubeFreak",
    "TauschbörseMeister",
    "SammlerGlück123",
    "GünstigVerkauf321",
    "RetroSammlerin99",
    "Schnäppchenjagd007",
    "Verkaufskönigin22",
    "SammelLeidenschaftPro",
    "MarktPlatzGuru",
    "TrödelLiebhaber007",
    "PlauschTauschProfi55",
    "VintageLiebhaber44",
    "HandelsGuru66",
    "Angebotssucherin11",
    "PlauschTauschKönig",
    "BieteUndSuchePro",
    "SchnäppchenFan112",
    "FlohmarktFee88",
    "VerkaufsFreak2023",
    "FundgrubenQueen",
    "TauschbörseExperte",
    "Sammelwut123",
    "PlauschTauschChampion",
    "VintageSchätze77",
    "GünstigDeal123",
    "RetroFan456",
    "AngebotssucherPro",
    "Handelsgenie22",
    "VerkaufsProfi99",
    "SchnäppchenLiebhaberin",
    "FlohmarktEntdecker",
    "FundgrubenKönig",
    "TauschbörseExperte55",
    "SammelFreude123",
    "PlauschTauschSchatz",
    "Verkaufsmeister123",
    "MarktPlatzHeld33",
    "GünstigDeal007",
    "RetroSammler55",
    "PlauschTauschPro777",
    "VintageLiebhaberin22",
    "Angebotssucherin44",
    "TrödelHeldin11",
    "Schnäppchenfuchs99",
    "FlohmarktKönigin77",
    "VerkaufsVirtuose2023",
    "FundgrubenSchatz",
    "TauschbörseKing123",
    "SammelGlück456",
    "PlauschTauschKingpin",
    "GünstigVerkauf22",
    "RetroLiebhaberin55",
    "Schnäppchenjägerin007",
    "VerkaufsQueen22",
    "AngebotssucherExpert",
    "HandelsProfi123",
    "MarktPlatzFreak",
    "TrödelLiebhaberin66",
    "PlauschTauschMeister22",
    "VintageFan777",
    "GünstigDealPro99",
    "SchnäppchenSucherin33",
    "FlohmarktFee2023",
    "VerkaufsChampion123",
    "FundgrubenKönigin55",
    "TauschbörseSpezialist",
    "SammelLeidenschaftQueen",
    "PlauschTauschSammler22",
    "RetroLiebhaberin99",
    "Angebotssucherin22",
    "TrödelProfi007",
    "SchnäppchenFan88",
    "FlohmarktSchatz123",
    "VerkaufsExperte22",
    "FundgrubenKönig88",
    "TauschbörseMeister77",
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

    public static string GetRandomUsername()
    {
        int rand = Random.Range(0, traderNames.Length);
        return traderNames[rand];
    }
}
