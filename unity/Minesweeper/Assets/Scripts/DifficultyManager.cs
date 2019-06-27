using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DifficultyManager : Singleton<DifficultyManager>
{
    //GLOBAL FUNCTIONS FOR EVERY DIFFICULTY


    public InputField width;
    public InputField heigth;
    public InputField bombs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Beginner()
    {
        width.text = "9";
        heigth.text = "9";
        bombs.text = "10";
        //GenarateMap.Instance().width = 9;
        //GenarateMap.Instance().heigth = 9;
        //GenarateMap.Instance().bombCount = 10;
        Custom();
        //GenarateMap.Instance().GenerateMap();
    }

    public void Intermidate()
    {
        width.text = "16";
        heigth.text = "16";
        bombs.text = "40";
        //GenarateMap.Instance().width = 16;
        //GenarateMap.Instance().heigth = 16;
        //GenarateMap.Instance().bombCount = 40;
        Custom();
        //GenarateMap.Instance().GenerateMap();
    }

    public void Expert()
    {
        width.text = "30";
        heigth.text = "16";
        bombs.text = "99";
        //GenarateMap.Instance().width = 30;
        //GenarateMap.Instance().heigth = 16;
        //GenarateMap.Instance().bombCount = 99;
        Custom();
        //GenarateMap.Instance().GenerateMap();
    }

    public void Custom()
    {
        int.TryParse(width.text, out GenarateMap.Instance().width);
        int.TryParse(bombs.text, out GenarateMap.Instance().bombCount);
        int.TryParse(heigth.text, out GenarateMap.Instance().heigth);
        //GenarateMap.Instance().GenerateMap();
    }
}
