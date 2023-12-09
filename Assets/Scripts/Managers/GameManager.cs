using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public  class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [NonSerialized]
    public float currency;
    public int currentPhase;
    public float currencyLimit;
    public string locationName;
    public TextMeshProUGUI currencyDidsplay;
    public TextMeshProUGUI locationDisplay;
    bool cheats;
    private void Awake() {
        instance = this;
        currency = 0;
        locationDisplay.text = locationName;
        if(currentPhase > 2) currentPhase = 2;
    }
    private void Update() {
        if(currency>=currencyLimit) currencyDidsplay.text = Utilities.ToCurrency(currency) + " (Max)";
        else currencyDidsplay.text = Utilities.ToCurrency(currency);

        if(Input.GetKeyDown(KeyCode.E))
        {
            cheats = !cheats;
        }

        if(Input.GetKeyDown(KeyCode.Alpha0) && !cheats)
        {
            currency = 0;
        }

        if(cheats) currency = currencyLimit;
    }
    public void AddCurrency(float newCurrency)
    {
        currency += newCurrency;
        currency = Mathf.Clamp(currency,0,currencyLimit);
    }
    public void SetCurrencyLimit(float limit)
    {
        currencyLimit = limit;
    }
}
