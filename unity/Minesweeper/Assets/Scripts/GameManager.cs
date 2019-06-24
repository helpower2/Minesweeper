using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GenarateMap mapGenaretor;
    [SerializeField] private ClickManager clickManager;
    public UnityMineDataEvent OnMineDataHit = new UnityMineDataEvent();
    public UnityMineDataEvent OnBombHit = new UnityMineDataEvent();
    public Camera mainCamare;
    public float camareScale = 1.8f;

    // Start is called before the first frame update
    void Start()
    {
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

        ScoreManager scoreManager = ScoreManager.Instance();
        scoreManager.score+=1;
    }

    public void RevealMineData(MineData mineData)
    {
        mineData.isRevealed = true;
        mineData.UpdateGraphics();
    }

    public void RevealAllMinedatas()
    {
        mapGenaretor.MineDatas.OfType<MineData>().ToList().ForEach((x) => { RevealMineData(x); });
    }
    public void delay()
    {
        //Delayer.Instance().SetDelay(1, RevealAllMinedatas);
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
