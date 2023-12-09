using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    static float offerConstant = 0.25f;
    static float levelConstant = 1.13f;
    public static string ToCurrency(float amount)
    {
        return ToCurrencyType(ToNumber(amount));
    }
    public static string ToNumber(float amount)
    {
        string text = "";
        if (amount > 1000000000000000) text = (amount / 1000000000000000).ToString("F1") + "mB";
        else if (amount > 1000000000000) text = (amount / 1000000000000).ToString("F1") + "B";
        else if (amount > 1000000000) text = (amount / 1000000000).ToString("F1") + "mM";
        else if (amount > 1000000) text = (amount / 1000000).ToString("F1") + "M";
        else if (amount > 1000) text = (amount / 1000).ToString("F1") + "m";
        else text = amount.ToString();
        return text;
    }
    public static string ToCurrencyType(string amount)
    {
        string text = amount + "<sprite index="+GameManager.instance.currentPhase+">";
        return text;
    }
    public static float GetOfferConstant()
    {
        return offerConstant;
    }
    public static float GetLevelConstant()
    {
        return levelConstant;
    }
}
