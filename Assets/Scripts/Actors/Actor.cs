using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Actor : MonoBehaviour
{
    public ActorData actorData;
    [Header("Stats")]
    public float mainBarAmount;
    public float mainBarTime;
    public float actorAmount;
    public float autoBarTime;

    [Header("Limits")]
    public float autoBarLimitTime;
    public float mainBarLimitTime;

    [Header("UI")]
    public Image autoBarImage;
    public TextMeshProUGUI autoBarText;
    public Image mainBarImage;
    public TextMeshProUGUI mainBarText;
    public Image actorImage;
    public TextMeshProUGUI actorText;
    public TextMeshProUGUI actorAmountText;
    public Animator animator;
    
    //Timers
    float autoBarTimer;
    bool onProces;
    
    private void Start() 
    {
        if(actorData!=null)
        {
            mainBarAmount = actorData.mainBarAmount;
            autoBarTime = actorData.autoBarTime;
            mainBarTime = actorData.mainBarTime;
            actorAmount = actorData.actorAmount;
            autoBarLimitTime = actorData.autoBarLimitTime;
            mainBarLimitTime = actorData.mainBarLimitTime;
            actorImage.sprite = actorData.actorIcon;
            actorText.text = actorData.actorName;
        }
        mainBarText.text = Utilities.ToCurrency(mainBarAmount);
        actorAmountText.text = actorAmount.ToString();
        autoBarText.text =  Utilities.ToCurrency(mainBarAmount*actorAmount/autoBarTime) + " / s";
        mainBarImage.fillAmount = 0;

    }

    private void Update() {
        UpdateAutoBar();
    }

    //Setters
    public void SetMainBarAmount(float newMainBarAmount)
    {
        mainBarAmount = newMainBarAmount;
        mainBarText.text = Utilities.ToCurrency(mainBarAmount);
        SetAutoBarTime(autoBarTime);
    }
    public void SetMainBarTime(float newMainBarTime)
    {
        mainBarTime = newMainBarTime;
    }
    public void SetAutoBarTime(float newAutoBarTime)
    {
        autoBarTime = newAutoBarTime;
        autoBarText.text =  Utilities.ToCurrency(mainBarAmount*actorAmount/autoBarTime) + " / s";
    }
    public void SetActorAmount(float newActorAmount)
    {
        actorAmount = newActorAmount;
        actorAmountText.text = actorAmount.ToString();
        SetAutoBarTime(autoBarTime);
    }

    //Update
    public void UpdateAutoBar()
    {
        float maxTime = Mathf.Clamp(autoBarTime,autoBarLimitTime,Mathf.Infinity);
        if(autoBarTimer < maxTime)
        {
            autoBarTimer+=Time.deltaTime;
            autoBarImage.fillAmount = autoBarTimer/maxTime;
        }
        else
        {
            autoBarTimer = 0;
            autoBarImage.fillAmount = autoBarTimer/maxTime;
            float amountCurrency = mainBarAmount;
            if(autoBarTime < autoBarLimitTime) amountCurrency*=autoBarLimitTime/autoBarTime;
            GameManager.instance.AddCurrency(amountCurrency);
        }
        
    }
    public void PressActor()
    {
        if(!onProces) StartCoroutine(UpdateMainBar());
    }
    IEnumerator UpdateMainBar()
    {
        onProces = true;
        animator.SetTrigger("Pressed");
        float maxTime = Mathf.Clamp(mainBarTime,mainBarLimitTime,Mathf.Infinity);
        float timer = 0;
        mainBarImage.fillAmount = 0;
        while(timer < maxTime)
        {
            timer+=Time.deltaTime;
            mainBarImage.fillAmount = timer/maxTime;
            yield return null;
        }
        mainBarImage.fillAmount = 0;
        float amountCurrency = mainBarAmount;
        if(mainBarTime < mainBarLimitTime) amountCurrency*=mainBarLimitTime/mainBarTime;
        GameManager.instance.AddCurrency(amountCurrency);
        onProces = false;
    }
}
