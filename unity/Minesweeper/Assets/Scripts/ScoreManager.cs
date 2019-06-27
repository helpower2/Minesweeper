using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int score = 0;

    

    void Start()
    {
        GameManager.Instance();
    }

    /// <summary>
    /// score for every good block clicked
    /// </summary>
    /// <param name="score"></param>
    public void IncreaseScore(int score = 1)
    {
        score += score;
    }
}
