using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GenarateMap : Singleton<GenarateMap>
{
    public int width = 20;
    public int heigth = 20;
    public int bombCount = 50;
    public GameObject prefab;
    public Transform map;
    public bool DebugMode = false;
    public UnityEvent onMapGenerate = new UnityEvent();


    [SerializeField] private MineData[,] mineDatas;

    //property value, zelf weten wat je returnt
    public MineData[,] MineDatas { get { return mineDatas; }  set { mineDatas = value; } }



    /// <summary>
    /// generates the map and fills it
    /// </summary>
    public void GenerateMap()
    {

        MineDatas = new MineData[heigth, width];
        map.DestroyChilds(); //kill the earth
        GenerateEmptyMap();
        FillMap();
        onMapGenerate.Invoke();
    }

    /// <summary>
    /// makes and empty map without anything
    /// </summary>
    public void GenerateEmptyMap()
    {

        map.DestroyChilds(); //kill the earth
        for (int h = 0; h < heigth; h++)
        {
            for (int w = 0; w < width; w++)
            {
                GameObject go = Instantiate(prefab, new Vector2( w - (width / 2),h - (heigth / 2)), Quaternion.identity, map);
                go.name += $@"{h}, {w}";
                MineData data = go.GetComponent<MineData>();
                MineDatas[h, w] = data;
                data.localPos = new Vector2Int(h, w);
            }
        }
    }

    /// <summary>
    /// Fills the map with bombs
    /// </summary>
    public void FillMap()
    {
        if (heigth * width <= bombCount) Debug.LogError("Too many bombs");
        for (int i = 0; i < bombCount; i++)
        {
            ActivateRandomeMine();
        }
        MineDatas.OfType<MineData>().ToList().ForEach((x) => { x.StartBombCount(); x.UpdateGraphics(); });
    }

    /// <summary>
    /// check if clickable is a bomb, then Invokes the function ActivateRandomeMine()
    /// </summary>
    public void ActivateRandomeMine()
    {
        Vector2Int vec = new Vector2Int(Random.Range(0, heigth), Random.Range(0, width));
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
