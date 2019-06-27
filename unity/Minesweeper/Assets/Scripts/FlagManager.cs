using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagManager : Singleton<FlagManager>
{

    //counter van de flag en een text element die het laat zien en update
    [SerializeField] private int FlagCount = 10;
    public Text flagcountText;

    //Increased of Decreased de flagcounter en update het text element
    public void IncreaseFlagCount(int IncreaseFlag = 1)
    {
        FlagCount += IncreaseFlag;

        flagcountText.text = "" + FlagCount;
    }

    /// <summary>
    /// restart de map en maakt een nieuwe als de size is veranderd
    /// </summary>
    public void Restart()
    {
        GameManager.Instance().won = true;

        if(GameManager.Instance().won == true)
        {
            GetComponent<ClickManager>().enabled = true;
            GetComponent<ClickManagerLeft>().enabled = true;
        }
        FlagCount = GenarateMap.Instance().bombCount;
    }
    public void Start()
    {
        Restart();
        flagcountText.text = "" + FlagCount;
    }
}
