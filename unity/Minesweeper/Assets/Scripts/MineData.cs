using System;
using System.Collections;
using System.Collections.Generic;
using Saving;
using UnityEngine;
[RequireComponent(typeof(Clickable))]
public class MineData : MonoBehaviour
{
    public bool isBomb = false;
    public bool hasFlag = false;
    public int totalbombsNearby;
    public bool isRevealed = false;
    public Vector2Int localPos;
    [SerializeField] private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
    }

    //starts the bomb count
    public void StartBombCount()
    {
        StartCoroutine(GetTotalbombsNearby());
    }

    /// <summary>
    /// checks if clickable is revealed or is a bomb, then reveal all neighbours
    /// </summary>
    public void showNullNeighbors()
    {
        if (isBomb || totalbombsNearby != 0) return;
        foreach (var item in GetNeighbors())
        {
            if (item == null) continue;
            if (item.isRevealed || item.isBomb) continue;
            item.Reveal();
            item.showNullNeighbors();
           
        }
    }

    public void Reveal()
    {
        isRevealed = true;
        UpdateGraphics();
    }
    /// <summary>
    /// gets all neighbours
    /// </summary>
    /// <returns>neighbors</returns>
    public MineData[,] GetNeighbors()
    {
        var map = GenarateMap.Instance().MineDatas;
        MineData[,] temp = new MineData[3,3];
        for (int W = localPos.x - 1, w = 0; W <= localPos.x + 1; W++, w++)
        {
            for (int H = localPos.y - 1, h = 0; H <= localPos.y + 1; H++, h++)
            {
                if ((H >= 0 && W >= 0) && (W < map.GetLength(0) && H < map.GetLength(1)))
                {
                    temp[w, h] = map[W, H];
                }
            }
        }
        return temp;
    }

    /// <summary>
    /// gets the count of the total bombs nearby
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetTotalbombsNearby()
    {
        yield return 0;//waits 1 frame
        var map = GenarateMap.Instance().MineDatas;
        if (isBomb)
        {
            // Debug.Log("Bomb");
        }
        else
        {
            //Debug.Log("counting");
            for (int W = localPos.x - 1; W <= localPos.x + 1; W++)
            {
                for (int H = localPos.y - 1; H <= localPos.y + 1; H++)
                {
                    if ((H >= 0 && W >= 0) && (W < map.GetLength(0) && H < map.GetLength(1)))
                    {                        //Debug.Log(W + " " + H);
                        if (map[W, H].isBomb)
                        {
                            totalbombsNearby++;
                        }
                        //Debug.Log("inside map");
                    }
                    else
                    {
                        //outside the map 
                        //Debug.Log("outside map");
                        continue;
                    }
                }
            }
            //Debug.Log(totalbombsNearby);
            UpdateGraphics();
        }
    }

    /// <summary>
    /// updates colors and sprites
    /// </summary>
    public void UpdateGraphics()
    {

        if (hasFlag)
        {
            spriteRenderer.SetSprite(SpriteReverence.Instance().Flag);
            return;
        }
        if (!isRevealed)
        {
            spriteRenderer.sprite = SpriteReverence.Instance().NotRevealed;
        }
        else if (isBomb)
        {
            spriteRenderer.sprite = SpriteReverence.Instance().Bomb;
            //Debug.Log("Bomb");
        }
        else
        {
            spriteRenderer.sprite = SpriteReverence.Instance().sprites[totalbombsNearby];

            spriteRenderer.SetColor(Color.Lerp(Color.white, Color.red,(float) (totalbombsNearby / 5f)));
        }
        if (spriteRenderer.sprite == null && !this.isRevealed)
        {
            spriteRenderer.SetColor(Color.gray);
        }
    }


    /// <summary>
    /// all variables to minedata variables
    /// </summary>
    /// <param name="mineDataSave"></param>
    public void LoadMineDataSave(SaveFile.MineDataSave mineDataSave)
    {
        isBomb = mineDataSave.isBomb;
        isRevealed = mineDataSave.isRevealed;
        totalbombsNearby = mineDataSave.totalbombsNearby;
        UpdateGraphics();
    }

    /// <summary>
    /// toggles the flag / updates the sprites / and decrease or increase the flag count
    /// </summary>
    public void ToggleFlag()
    {
        hasFlag = !hasFlag;
        UpdateGraphics();
        FlagManager.Instance()?.IncreaseFlagCount((hasFlag == true) ?  -1 : 1);
    }
}
