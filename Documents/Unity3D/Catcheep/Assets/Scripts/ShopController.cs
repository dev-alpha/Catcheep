﻿using System;
using System.Collections;
using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [Header("Shop Menu UI")]
    public GameObject cashText;
    public GameObject MenuCashText;
    public GameObject BuyShipButtonGameObject;
    public GameObject ShopMenuGameObject;
    public GameObject HelpToolsGameObject;
    public GameObject EnergyGameObject;
    public GameObject WheelOfFortuneGameObject;
    public GameObject DecisionPanel;
    public GameObject AdPanel;
    public GameObject moneyPanel;

    private music musicManager;

    // Use this for initialization
    void Start()
    {
        Advertisement.Initialize("1453095");
        musicManager = GameObject.Find("Music Manager").GetComponent<music>();
        cashUpdate(PlayerPrefs.GetInt("money"));
        HelpToolsGameObject.transform.GetChild(0).transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "" + PlayerPrefs.GetInt("hayStackStock");
        HelpToolsGameObject.transform.GetChild(1).transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "" + PlayerPrefs.GetInt("netStock");
        HelpToolsGameObject.transform.GetChild(2).transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "" + PlayerPrefs.GetInt("loveStock");
        settingShipStatus();
    }

    void settingShipStatus()
    {
        for (int i = 1; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("ship" + i) == 1)
            {
                BuyShipButtonGameObject.transform.GetChild(0).transform.GetChild(i).transform.GetChild(2)
                    .GetComponentInChildren<TextMeshProUGUI>().text = "Sold";
            }    
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void moreInfo()
    {
        musicManager.UISFX(0);
        SceneManager.LoadScene("Credit");
    }

    public void buyShipButton()
    {
        musicManager.UISFX(1);
        StartCoroutine(objectOpened(BuyShipButtonGameObject));
        StartCoroutine(objectClosed(ShopMenuGameObject));
    }

    public void returnBuyShip()
    {
        musicManager.UISFX(1);
        StartCoroutine(objectClosed(BuyShipButtonGameObject));
        StartCoroutine(objectOpened(ShopMenuGameObject));
    }

    public void helpToolButton()
    {
        musicManager.UISFX(1);
        StartCoroutine(objectClosed(ShopMenuGameObject));
        StartCoroutine(objectOpened(HelpToolsGameObject));
    }

    public void returnHelpTool()
    {
        musicManager.UISFX(1);
        StartCoroutine(objectClosed(HelpToolsGameObject));
        StartCoroutine(objectOpened(ShopMenuGameObject));
    }

    public void wheelOfFortuneButton()
    {
        musicManager.UISFX(1);
        StartCoroutine(objectClosed(ShopMenuGameObject));
        StartCoroutine(objectOpened(WheelOfFortuneGameObject));
    }

    public void wheelOfFortuneReturn()
    {
        musicManager.UISFX(1);
        StartCoroutine(objectClosed(WheelOfFortuneGameObject));
        StartCoroutine(objectOpened(ShopMenuGameObject));
    }

    public void energyDringButtonClick()
    {
        musicManager.UISFX(1);
        StartCoroutine(objectClosed(ShopMenuGameObject));
        StartCoroutine(objectOpened(EnergyGameObject));
    }

    public void energyDringButtonOff()
    {
        musicManager.UISFX(1);
        StartCoroutine(objectClosed(EnergyGameObject));
        StartCoroutine(objectOpened(ShopMenuGameObject));
    }

    public void buyALifeButton()
    {
        if (PlayerPrefs.GetInt("money") >= 7500 && PlayerPrefs.GetInt("hearts") < PlayerPrefs.GetInt("maxHearts"))
        {
            musicManager.UISFX(1);
            StartCoroutine(objectOpened(DecisionPanel));
            DecisionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text =
                "Do you wanna buy an energy drink ?";
            DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(buyALife);
        }
    }

    public void CloseDecisionPanel()
    {
        musicManager.UISFX(2);
        StartCoroutine(objectClosed(DecisionPanel));
    }

    public void CloseAdsPanel()
    {
        musicManager.UISFX(2);
        StartCoroutine(objectClosed(AdPanel));
    }
    public void CloseMoneyPanel()
    {
        musicManager.UISFX(2);
        StartCoroutine(objectClosed(moneyPanel));
    }
    public void OpenMoneyPanel()
    {
        musicManager.UISFX(1);
        StartCoroutine(objectOpened(moneyPanel));
    }

    void buyALife()
    {
        musicManager.UISFX(0);
        DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - 7500);
        cashUpdate(PlayerPrefs.GetInt("money"));
        PlayerPrefs.SetInt("hearts", PlayerPrefs.GetInt("hearts") + 1);
        GameObject.Find("hearts").GetComponent<TextMeshProUGUI>().text =
            "x" + PlayerPrefs.GetInt("hearts");
        StartCoroutine(objectClosed(DecisionPanel));
    }

    public void killTimeButton()
    {
        if (PlayerPrefs.GetInt("money") >= 3000 && PlayerPrefs.GetFloat("heartTime") >= 1)
        {
            musicManager.UISFX(1);
            StartCoroutine(objectOpened(DecisionPanel));
            DecisionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text =
                "Do you wanna set the energy drink timer to 00:00 ?";
            DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(killTime);
        }
        else
        {
            musicManager.UISFX(1);
            StartCoroutine(objectOpened(moneyPanel));
        }
    }

    void killTime()
    {
        musicManager.UISFX(0);
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - 3000);
        PlayerPrefs.SetFloat("heartTime", -1);
        cashUpdate(PlayerPrefs.GetInt("money"));
        DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        StartCoroutine(objectClosed(DecisionPanel));
    }

    public void moreCapacityButton()
    {
        musicManager.UISFX(1);
        if (PlayerPrefs.GetInt("money") >= 12500 && PlayerPrefs.GetInt("maxHearts") < 10)
        {
            StartCoroutine(objectOpened(DecisionPanel));
            DecisionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text =
                "Do you wanna buy more energy drinks capacity (+1) ?";
            DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(moreCapacity);
        }
        else
        {
            StartCoroutine(objectOpened(moneyPanel));
        }
    }

    void moreCapacity()
    {
        musicManager.UISFX(0);
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - 12500);
        PlayerPrefs.SetInt("maxHearts", PlayerPrefs.GetInt("maxHearts") + 1);
        cashUpdate(PlayerPrefs.GetInt("money"));
        DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        StartCoroutine(objectClosed(DecisionPanel));
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }

    public void shipOneButton()
    {
        musicManager.UISFX(1);
        StartCoroutine(objectOpened(DecisionPanel));
        DecisionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text =
            "Do you Set this Ship as Default ship ?";
        DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(ShipOne);
    }

    void ShipOne()
    {
        musicManager.UISFX(0);
        PlayerPrefs.SetInt("ship", 1);
        DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        StartCoroutine(objectClosed(DecisionPanel));
    }

    public void shipTwoButton()
    {
        musicManager.UISFX(1);
        if (PlayerPrefs.GetInt("money") >= 15000)
        {
            StartCoroutine(objectOpened(DecisionPanel));
            DecisionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text =
                "Do you Set this Ship as Default ship ?";
            DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(ShipTwo);
        }
        else
        {
            StartCoroutine(objectOpened(moneyPanel));
            StartCoroutine(objectClosed(DecisionPanel));
        }
    }

    void ShipTwo()
    {
        musicManager.UISFX(0);
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - 15000);
        cashUpdate(PlayerPrefs.GetInt("money"));
        PlayerPrefs.SetInt("ship", 2);
        PlayerPrefs.SetInt("ship1", 1);
        DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        settingShipStatus();
        StartCoroutine(objectClosed(DecisionPanel));
    }

    public void shipThreeButton()
    {
        musicManager.UISFX(1);
        if (PlayerPrefs.GetInt("money") >= 25000)
        {
            StartCoroutine(objectOpened(DecisionPanel));
            DecisionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text =
                "Do you Set this Ship as Default ship ?";
            DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(ShipThree);
        }
        else
        {
            StartCoroutine(objectOpened(moneyPanel));
            StartCoroutine(objectClosed(DecisionPanel));
        }
    }

    void ShipThree()
    {
        musicManager.UISFX(0);
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - 25000);
        cashUpdate(PlayerPrefs.GetInt("money"));
        PlayerPrefs.SetInt("ship", 3);
        PlayerPrefs.SetInt("ship2", 1);
        DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        settingShipStatus();
        StartCoroutine(objectClosed(DecisionPanel));
    }

    public void shipFourButton()
    {
        musicManager.UISFX(1);
        if (PlayerPrefs.GetInt("money") >= 40000 )
        {
            StartCoroutine(objectOpened(DecisionPanel));
            DecisionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text =
                "Do you Set this Ship as Default ship ?";
            DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(ShipFour);
        }
        else
        {
            StartCoroutine(objectOpened(moneyPanel));
            StartCoroutine(objectClosed(DecisionPanel));
        }
    }

    void ShipFour()
    {
        musicManager.UISFX(0);
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - 40000);
        cashUpdate(PlayerPrefs.GetInt("money"));
        PlayerPrefs.SetInt("ship", 4);
        PlayerPrefs.SetInt("ship3", 1);
        DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        settingShipStatus();
        StartCoroutine(objectClosed(DecisionPanel));
    }

    public void buyHayButton()
    {
        musicManager.UISFX(1);
        if (PlayerPrefs.GetInt("money") >= 15000)
        {
            StartCoroutine(objectOpened(DecisionPanel));
            DecisionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text =
                "Do you wanna buy a HayStack (+1) ?";
            DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(buyHayStack);
        }
        else
        {
            StartCoroutine(objectOpened(moneyPanel));
            StartCoroutine(objectClosed(DecisionPanel));
        }
    }

    void buyHayStack()
    {
        musicManager.UISFX(0);
        PlayerPrefs.SetInt("hayStackStock", PlayerPrefs.GetInt("hayStackStock") + 1);
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - 15000);
        cashUpdate(PlayerPrefs.GetInt("money"));
        DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        HelpToolsGameObject.transform.GetChild(0).transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "" + PlayerPrefs.GetInt("hayStackStock");
        StartCoroutine(objectClosed(DecisionPanel));
    }

    public void buyNetButton()
    {
        musicManager.UISFX(1);
        if (PlayerPrefs.GetInt("money") >= 20000)
        {
            StartCoroutine(objectOpened(DecisionPanel));
            DecisionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text =
                "Do you wanna buy a Net (+1) ?";
            DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(buyNet);
        }
        else
        {
            StartCoroutine(objectOpened(moneyPanel));
            StartCoroutine(objectClosed(DecisionPanel));
        }
    }

    public void buyNet()
    {
        musicManager.UISFX(0);
        PlayerPrefs.SetInt("netStock", PlayerPrefs.GetInt("netStock") + 1);
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - 20000);
        cashUpdate(PlayerPrefs.GetInt("money"));
        HelpToolsGameObject.transform.GetChild(1).transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "" + PlayerPrefs.GetInt("netStock");
        DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        StartCoroutine(objectClosed(DecisionPanel));
    }

    public void buyLoveButton()
    {
        musicManager.UISFX(1);
        if (PlayerPrefs.GetInt("money") >= 30000)
        {
            StartCoroutine(objectOpened(DecisionPanel));
            DecisionPanel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text =
                "Do you wanna buy a Female sheepy (+1) ?";
            DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(buyLove);
        }
        else
        {
            StartCoroutine(objectOpened(moneyPanel));
            StartCoroutine(objectClosed(DecisionPanel));
        }
    }

    public void buyLove()
    {
        musicManager.UISFX(0);
        PlayerPrefs.SetInt("loveStock", PlayerPrefs.GetInt("loveStock") + 1);
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") - 30000);
        HelpToolsGameObject.transform.GetChild(2).transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "" + PlayerPrefs.GetInt("loveStock");
        cashUpdate(PlayerPrefs.GetInt("money"));
        DecisionPanel.transform.GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
        StartCoroutine(objectClosed(DecisionPanel));
    }

    void cashUpdate(int current)
    {
        musicManager.UISFX(3);
        cashText.GetComponent<TextMeshProUGUI>().text = "$" + current;
        MenuCashText.GetComponent<TextMeshProUGUI>().text = "$" + current;
    }

    public void facebook()
    {
        Application.OpenURL("fb://page/344400029290078");
    }

    IEnumerator objectOpened(GameObject objectToOpen)
    {
        objectToOpen.SetActive(true);
        for (int i = 0; i <= 10; i++)
        {
            float a = (float) i / 10;
            objectToOpen.transform.localScale = new Vector3(a, a, 1);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator objectClosed(GameObject objectToOpen)
    {
        for (int i = 10; i >= 0; i--)
        {
            float a = (float) i / 10;
            objectToOpen.transform.localScale = new Vector3(a, a, 1);
            yield return new WaitForSeconds(0.01f);
        }
        objectToOpen.SetActive(false);
    }

    //ADS SECTION
    public void hayStackAdFunction()
    {
        ShowRewardedVideo(0);
    }
    public void netAdFunction()
    {
        ShowRewardedVideo(1);
    }
    public void loveAdFunction()
    {
        ShowRewardedVideo(2);
    }
    public void shipTwoAdFunction()
    {
        ShowRewardedVideo(3);
    }
    public void shipThreeAdFunction()
    {
        ShowRewardedVideo(4);
    }
    public void shipFourAdFunction()
    {
        ShowRewardedVideo(5);
    }
    public void buyALifeAdFunction()
    {
        ShowRewardedVideo(6);
    }
    public void killTimeAdFunction()
    {
        ShowRewardedVideo(7);
    }
    public void moreCapacityAdFunction()
    {
        ShowRewardedVideo(8);
    }
    void ShowRewardedVideo(int index)
    {
        ShowOptions options = new ShowOptions();
        switch (index)
        {
            case 0:
                options.resultCallback = hayAd;
                break;
            case 1:
                options.resultCallback = netAd;
                break;
            case 2:
                options.resultCallback = loveAd;
                break;
            case 3:
                options.resultCallback = shipTwoAd;
                break;
            case 4:
                options.resultCallback = shipThreeAd;
                break;
            case 5:
                options.resultCallback = shipFourAd;
                break;
            case 6:
                options.resultCallback = buyALifeAd;
                break;
            case 7:
                options.resultCallback = killTimeAd;
                break;
            case 8:
                options.resultCallback = moreCapacityAd;
                break;
        }
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", options);
        }
        else
        {
            StartCoroutine(objectOpened(AdPanel));
        }
    }

    void hayAd(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            PlayerPrefs.SetInt("hayStackStock", PlayerPrefs.GetInt("hayStackStock") + 1);
            HelpToolsGameObject.transform.GetChild(0).transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "" + PlayerPrefs.GetInt("hayStackStock");
        }
    }
    void netAd(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            PlayerPrefs.SetInt("netStock", PlayerPrefs.GetInt("netStock") + 1);
            HelpToolsGameObject.transform.GetChild(1).transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "" + PlayerPrefs.GetInt("netStock");
        }
    }
    void loveAd(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            PlayerPrefs.SetInt("loveStock", PlayerPrefs.GetInt("loveStock") + 1);
            HelpToolsGameObject.transform.GetChild(2).transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "" + PlayerPrefs.GetInt("loveStock");
        }
    }
    void shipTwoAd(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            PlayerPrefs.SetInt("ship", 2);
            PlayerPrefs.SetInt("ship1", 1);
            settingShipStatus();
        }
    }
    void shipThreeAd(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            PlayerPrefs.SetInt("ship", 3);
            PlayerPrefs.SetInt("ship2", 1);
            settingShipStatus();
        }
    }
    void shipFourAd(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            PlayerPrefs.SetInt("ship", 4);
            PlayerPrefs.SetInt("ship3", 1);
            settingShipStatus();
        }
    }
    void moreCapacityAd(ShowResult result)
    {
        if (result == ShowResult.Finished && PlayerPrefs.GetInt("maxHearts") < 10)
        {
            PlayerPrefs.SetInt("maxHearts", PlayerPrefs.GetInt("maxHearts") + 1);
        }
    }
    void buyALifeAd(ShowResult result)
    {
        if (result == ShowResult.Finished && PlayerPrefs.GetInt("hearts") < PlayerPrefs.GetInt("maxHearts"))
        {
            PlayerPrefs.SetInt("hearts", PlayerPrefs.GetInt("hearts") + 1);
            GameObject.Find("hearts").GetComponent<TextMeshProUGUI>().text =
                "x" + PlayerPrefs.GetInt("hearts");
        }
    }
    void killTimeAd(ShowResult result)
    {
        if (result == ShowResult.Finished && PlayerPrefs.GetFloat("heartTime") >= 1)
        {
            PlayerPrefs.SetFloat("heartTime", -1);
            cashUpdate(PlayerPrefs.GetInt("money"));
        }
    }
}