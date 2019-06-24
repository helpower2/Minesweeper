using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteReverence : Singleton<SpriteReverence>
{
    public Sprite NotRevealed;
    public Sprite Bomb;
    public List<Sprite> sprites = new List<Sprite>(8);

    private void Awake()
    {
        SpriteReverence.Instance();
    }

}
