using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
