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
    public string levelName;
    public Camera mainCamare;
    public float camareScale = 1.8f;

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
    public void startGame()
    {
        mapGenaretor.GenerateMap();
    }

    public void Onclickable(GameObject clickable)
    { 
        var minedata = clickable.GetComponent<MineData>();
        if (minedata == null) return;
        if (minedata.isRevealed) return;
        OnMineDataHit.Invoke(minedata);
        if (minedata.isBomb) OnBombHit.Invoke(minedata);
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
        mainCamare.orthographicSize = Mathf.Max(mapGenaretor.with, mapGenaretor.hight) / camareScale;
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

