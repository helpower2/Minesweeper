using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DifficultyManager : Singleton<DifficultyManager>
{
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
        GenarateMap.Instance().width = 9;
        GenarateMap.Instance().heigth = 9;
        GenarateMap.Instance().bombCount = 10;
        GenarateMap.Instance().GenerateMap();
    }

    public void Intermidate()
    {
        GenarateMap.Instance().width = 16;
        GenarateMap.Instance().heigth = 16;
        GenarateMap.Instance().bombCount = 40;
        GenarateMap.Instance().GenerateMap();
    }

    public void Expert()
    {
        GenarateMap.Instance().width = 30;
        GenarateMap.Instance().heigth = 16;
        GenarateMap.Instance().bombCount = 99;
        GenarateMap.Instance().GenerateMap();
    }

    public void Custom()
    {
        int.TryParse(width.text, out GenarateMap.Instance().width);
        int.TryParse(bombs.text, out GenarateMap.Instance().bombCount);
        int.TryParse(heigth.text, out GenarateMap.Instance().heigth);
        GenarateMap.Instance().GenerateMap();
    }
}
