using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public static class Extensions
{
    // Generic
    public static void DestroyChilds(this Transform t)
    {
        for (int i = t.childCount - 1; i >= 0; i--)
            MonoBehaviour.Destroy(t.GetChild(i).gameObject);
    }

    public static void ChildsActiveState(this Transform t, bool state)
    {
        for (int i = t.childCount - 1; i >= 0; i--)
            t.GetChild(i).gameObject.SetActive(state);
    }

    public static void Reset(this Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localEulerAngles = Vector3.zero;
        t.localScale = Vector3.one;
    }

    // Spriterenderer
    public static void SetColor(this SpriteRenderer spriteRenderer, Color color)
    {
        spriteRenderer.color = color;
    }

    public static void SetSprite(this SpriteRenderer spriteRenderer, Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}