﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteReverence : Singleton<SpriteReverence>
{
    public Sprite NotRevealed;
    public Sprite Bomb;
    public Sprite Flag;
    public Sprite Smiley;
    public Sprite deadSmiley;
    public List<Sprite> sprites = new List<Sprite>(8);

    /// <summary>
    /// _instance vult hij en maakt de functie sneller
    /// </summary>
    private void Awake()
    {
        SpriteReverence.Instance();
    }

}
