using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Clickable))]
public class MineData : MonoBehaviour
{
    public bool isBomb = false;
    public int totalbombsNearby;
    public bool isRevealed = false;
    public Vector2Int localPos;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
    }
    public void StartBombCount()
    {
        StartCoroutine(GetTotalbombsNearby());
    }

    public IEnumerator GetTotalbombsNearby()
    {
        yield return 0;
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
                    {
                        //-1 , -1 kan er door komen. HOE??
                        //we are looking in the map 
                        //Debug.Log(W + " " + H);
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
    public void UpdateGraphics()
    {
        if (!isRevealed)
        {
            spriteRenderer.sprite = SpriteReverence._instance.NotRevealed;
        }
        else if (isBomb)
        {
            spriteRenderer.sprite = SpriteReverence._instance.Bomb;
            //Debug.Log("Bomb");
        }
        else
        {
            spriteRenderer.sprite = SpriteReverence._instance.sprites[totalbombsNearby];

            spriteRenderer.SetColor(Color.Lerp(Color.white, Color.red,(float) (totalbombsNearby / 5f)));
        }
    }
}
