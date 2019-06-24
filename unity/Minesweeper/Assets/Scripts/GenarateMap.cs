using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GenarateMap : Singleton<GenarateMap>
{
    public int with = 20;
    public int hight = 20;
    public int bombCount = 50;
    public GameObject prefab;
    public Transform map;
    public bool DebugMode = false;
    public UnityEvent onMapGenerate = new UnityEvent();


    [SerializeField] private MineData[,] mineDatas;
    public MineData[,] MineDatas { get { return mineDatas; } private set { mineDatas = value; } }




    public void GenerateMap()
    {
        MineDatas = new MineData[with, hight];
        map.DestroyChilds(); //kill the earth
        for (int h = 0; h < hight; h++)
        {
            for (int w = 0; w < with; w++)
            {
                GameObject go = Instantiate(prefab, new Vector2(h - (hight / 2), w - (with / 2)), Quaternion.identity, map);
                go.name += $@"{h}, {w}";
                MineData data = go.GetComponent<MineData>();
                MineDatas[h, w] = data;
                data.localPos = new Vector2Int(h, w);
            }
        }
        FillMap();
        onMapGenerate.Invoke();
    }
    public void FillMap()
    {
        if (hight * with <= bombCount) Debug.LogError("Too many bombs");
        for (int i = 0; i < bombCount; i++)
        {
            ActivateRandomeMine();
        }
        MineDatas.OfType<MineData>().ToList().ForEach((x) => { x.StartBombCount(); x.UpdateGraphics(); });
    }
    public void ActivateRandomeMine()
    {
        Vector2Int vec = new Vector2Int(Random.Range(0, hight), Random.Range(0, with));
        MineData mine = MineDatas[vec.x, vec.y];
        if (mine.isBomb)
        {
            ActivateRandomeMine();
        }
        else
        {
            mine.isBomb = true;
        }
    }
}
