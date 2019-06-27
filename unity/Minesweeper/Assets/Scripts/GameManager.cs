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
    public UnityEvent OnWon = new UnityEvent();
    public int MineshitThisGame = 0;
    public string levelName;
    public Camera mainCamare;
    public float camareScale = 1.8f;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = ScoreManager.Instance();
        if (clickManager == null) clickManager = GetComponent<ClickManager>();
        clickManager.OnClick.AddListener(Onclickable);
        OnMineDataHit.AddListener(delegate { scoreManager.IncreaseScore(1); } );
        OnBombHit.AddListener(delegate { scoreManager.IncreaseScore(-1); } );
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
        clickManager.enabled = true;
        ClickManagerLeft.Instance().enabled = true;

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

    public void RevealMineData(MineData mineData)
    {
        mineData.isRevealed = true;
        mineData.UpdateGraphics();
        if (mineData.totalbombsNearby == 0)
        {
            mineData.showNullNeighbors();
        }
    }

    public void RevealAllMinedatas()
    {
        mapGenaretor.MineDatas.OfType<MineData>().ToList().ForEach((x) => { RevealMineData(x); });
    }
    public void RevealAllMinedatas(MineData mineData)
    {
        RevealAllMinedatas();
    }
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

    public void WinFunction()
    {
        Debug.Log("win function");
        bool _won = true;
        mapGenaretor.MineDatas.OfType<MineData>().ToList().ForEach((x) => { if ((x.isBomb == true && x.hasFlag == true) || (x.isBomb == false && x.isRevealed == true && x.hasFlag == false)) {  } else { _won = false; } });
        if (_won)
        {
            Debug.Log("you won");
            clickManager.enabled = false;
            ClickManagerLeft.Instance().enabled = false;
            OnWon.Invoke();
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

