using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{

    public GenarateMap mapGenaretor;
    public ClickManager clickManager;
    public ScoreManager scoreManager;
    public UnityMineDataEvent OnMineDataHit = new UnityMineDataEvent();
    public UnityMineDataEvent OnBombHit = new UnityMineDataEvent();
    public UnityMineDataEvent OnMineDataLeft = new UnityMineDataEvent();
    public UnityEvent OnRestart = new UnityEvent();
    public int MineshitThisGame = 0;
    public string levelName;
    public Camera mainCamare;
    public float camareScale = 1.8f;
    public bool won = true;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = ScoreManager.Instance();
        if (clickManager == null) clickManager = GetComponent<ClickManager>();
        clickManager.OnClick.AddListener(Onclickable);
        if (mapGenaretor == null)
        {
            //Debug.LogError("mapGenaretor ");
        }
        startGame();
    }
    /// <summary>
    /// deze functie generate de map
    /// </summary>
    public void startGame()
    {
        mapGenaretor.GenerateMap();
    }

    public void Restart()
    {
        OnRestart.Invoke();
    }
    
    public void ResetMineHits()
    {
        MineshitThisGame = 0;
    }

    /// <summary>
    /// Invoke OnMineDataHit and OnBombHit arccordingly
    /// </summary>
    /// <param name="clickable"></param>
    public void Onclickable(GameObject clickable)
    { 
        var minedata = clickable.GetComponent<MineData>();
        if (minedata == null) return;
        if (minedata.isRevealed || minedata.hasFlag) return;
        OnMineDataHit.Invoke(minedata);
        if (minedata.isBomb) OnBombHit.Invoke(minedata);
    }

    /// <summary>
    /// Invokes OnMineDataLeft and will place a flag
    /// </summary>
    /// <param name="clickable"></param>
    public void OnLeftclickable(GameObject clickable)
    {

        var minedata = clickable.GetComponent<MineData>();
        if (minedata == null) return;
        if (minedata.isRevealed) return;
        OnMineDataLeft.Invoke(minedata);
    }

    /// <summary>
    /// reveals minedata, the 0's and 1's
    /// </summary>
    /// <param name="mineData"></param>
    public void RevealMineData(MineData mineData)
    {
        mineData.Reveal();
        if (mineData.totalbombsNearby == 0)
        {
            mineData.showNullNeighbors();
        }
    }

    /// <summary>
    /// Reveals everything.
    /// </summary>
    public void RevealAllMinedatas()
    {
        mapGenaretor.MineDatas.OfType<MineData>().ToList().ForEach((x) => { RevealMineData(x); });
    }
    public void RevealAllMinedatas(MineData mineData)
    {
        RevealAllMinedatas();
    }

    // sets the orthographic size of the camera.
    public void SetCamera()
    {
        mainCamare.orthographicSize = Mathf.Max(mapGenaretor.width, mapGenaretor.heigth) / camareScale;
    }

    public void SetLevelname(string name)
    {
        levelName = name;
    }
    public void ToggleFlag(MineData mineData)
    {
        mineData.ToggleFlag();
    }

    /// <summary>
    /// Win function if everything is filled except the bombs.
    /// </summary>
    /// <param name="mineData"></param>
    public void WinFunction(MineData mineData)
    {
        
        mapGenaretor.MineDatas.OfType<MineData>().ToList().ForEach((x) => { if (x.isBomb == true && x.isRevealed == false || x.isBomb == false && x.isRevealed == true) {  } else { won = false; } });
        if (won)
        {
            Debug.Log("you won");
            GetComponent<ClickManager>().enabled = false;
            GetComponent<ClickManagerLeft>().enabled = false;

        }
    }

    public void QuitFunction()
    {
        Application.Quit();
    }
}
[System.Serializable]
public class UnityMineDataEvent : UnityEvent<MineData> { }



//the GameManager will contain and controll the most importand things like the map genarater, clickmanager, camera, onMinedataHit, OnbombHit
//in the start it will get all of the importand things. and genarete the base map
//onclickable will take the gameobject of the clickmanager.onclick and use it to get the MineData and invoke the onMineDataHit and OnbombHit acoringly 
//the RevealMineData takes the onMineDataHit and reveal the data or bomb
//the RevealAllMinedatas reveals the whole map and is triggert with OnbombHit.
//the SetCamera function is run when the map is generated.

