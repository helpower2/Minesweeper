using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T _instance { get; private set; }

    public static T Instance()
    {
        if (_instance == null)
        {
            _instance = FindObjectOfType<T>();
        }

        return _instance;
    }
}