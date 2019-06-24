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
        OnMineDataHit.Invoke(minedata);
        if (minedata.isBomb) OnBombHit.Invoke(minedata);

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
}
[System.Serializable]
public class UnityMineDataEvent : UnityEvent<MineData> { }
