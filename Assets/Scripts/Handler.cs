using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Handler : MonoBehaviour
{
    public enum STATE
    {
        Setup,
        Act,
        fivestar
    }

    [Header("System Variables")]
    public STATE appState = STATE.Setup;

    public int pages;
    public int onpage;
    public int pity;

    public bool tenPull = false;

    [Header("Gameobjects")]
    public TextMeshProUGUI pageNum;
    public TextMeshProUGUI onPageNum;
    public Slider pageSlider;
    public Slider onPageSlider;

    public TextMeshProUGUI pityData;

    public Slider tenPullSlider;
    public TextMeshProUGUI tenPullNum;
    [Header("State Containers")]
    public GameObject setupState;
    public GameObject actState;
    public GameObject fivestarState;

    public GameObject prompt;
    public GameObject TENPULL;

    void Update()
    {
        stateManager();
        if (appState == STATE.Setup)
        {
            pageNum.text = "" + pageSlider.value;
            onPageNum.text = "" + onPageSlider.value;
        }
        else if (appState == STATE.Act)
        {
            pityData.text = "Total pity: " + pity + "\nHard pity in: " + (90 - pity) + "\nSoft pity in: " + (75 - pity);
        }
        else if (appState == STATE.fivestar)
        {
            tenPullNum.text = "" + tenPullSlider.value;
        }
    }

    public void yn(string yn)
    {
        if (yn == "n")
        {
            if (tenPull)
            {
                pity += 10;
                appState = STATE.Act;
            }
            else
            {
                pity++;
                appState = STATE.Act;
            }
        }
        else if (yn == "y")
        {
            if (tenPull)
            {
                prompt.SetActive(false);
                TENPULL.SetActive(true);
                pity = 0;
            }
            else
            {
                pity = 0;
                appState = STATE.Act;
            }
        }
    }

    public void tpfsConf()
    {
        int far = (int)tenPullSlider.value;
        pity = 10 - far;
        prompt.SetActive(true);
        TENPULL.SetActive(false);
        appState = STATE.Act;
        tenPullSlider.value = 1;
    }

    public void confirmSelection()
    {
        pages = (int)pageSlider.value;
        onpage = (int)onPageSlider.value;
        pity = pages * 6 + onpage;
        appState = STATE.Act;
    }

    public void pull(bool _tenPull)
    {
        if (_tenPull)
        {
            tenPull = true;
        }
        else tenPull = false;

        appState = STATE.fivestar;
    }

    public void stateManager()
    {
        if (appState == STATE.Setup)
        {
            setupState.SetActive(true);
            actState.SetActive(false);
            fivestarState.SetActive(false);
        }
        else if (appState == STATE.Act)
        {
            setupState.SetActive(false);
            actState.SetActive(true);
            fivestarState.SetActive(false);
        }
        else
        {
            setupState.SetActive(false);
            actState.SetActive(false);
            fivestarState.SetActive(true);
        }
    }
}
