using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is for saving the level
//I(Jeffrey) have made this becuase i'm bored

namespace Saving
{
    public class SaveSystem : Singleton<SaveSystem>
    {
        public SaveFile save = new SaveFile();
        private string dataPath;
        // Start is called before the first frame update
        IEnumerator Start()
        {
            dataPath = Application.dataPath + "\\save.json";
            yield return 0; //wait 1 frame 
            try
            {
                if (!File.Exists(dataPath))
                {
                    //file does not exist make a new one.
                    CreateSaveFile();
                }
                else
                {
                    //try to load file
                    LoadSaveFile();
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
            
        }

        private IEnumerator CreateSaveFile()
        {
            FileStream fileStream = File.Create(dataPath);
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.WriteLine(JsonUtility.ToJson(save));
            }
            yield return null;
        }

        public void LoadSaveFile()
        {
            JsonUtility.FromJsonOverwrite(File.ReadLines(dataPath).ToString(), save);
            return;
        }
        public void SaveSaveFile()
        {
            File.WriteAllText(dataPath, JsonUtility.ToJson(save));
        }
    }
    [System.Serializable]
    public class SaveFile
    {
        [System.Serializable]
        public struct MineDataSave
        {
            public bool isBomb;
            public int totalbombsNearby;
            public bool isRevealed;
            public Vector2Int localPos;

            public MineDataSave(MineData mineData)
            {
                isBomb = mineData.isBomb;
                totalbombsNearby = mineData.totalbombsNearby;
                isRevealed = mineData.isRevealed;
                localPos = mineData.localPos;
            }
            public static implicit operator MineDataSave(MineData mineData)
            {
                return new MineDataSave(mineData);
            }
        }
        [System.Serializable]
        public struct Level
        {
            public string levelName;
            public int with;
            public int hight;
            public int bombCount;
            public MineDataSave[,] level;
            public DateTime saveTime;
            public int score;
            public float time;

            public Level(GameManager gameManager)
            {
                this.levelName = gameManager.levelName;
                with = gameManager.mapGenaretor.with;
                hight = gameManager.mapGenaretor.hight;
                bombCount = gameManager.mapGenaretor.bombCount;
                level = new MineDataSave[gameManager.mapGenaretor.with, gameManager.mapGenaretor.hight]; for (int w = 0; w < gameManager.mapGenaretor.with; w++)
                {
                    for (int h = 0; h < gameManager.mapGenaretor.hight; h++)
                    {
                        level[w, h] = gameManager.mapGenaretor.MineDatas[w, h];
                    }
                }
                saveTime = DateTime.Now;
                score = gameManager.scoreManager.score;
                time = 1f;
            }
            public static implicit operator Level(GameManager gameManager)
            {
                return new Level(gameManager);
            }

        }
        public List<Level> levels;

        public void loadlevel(Level level)
        {
            GameManager gameManager = GameManager.Instance();
            gameManager.levelName = level.levelName;
            gameManager.mapGenaretor.with = level.with;
            gameManager.mapGenaretor.hight = level.hight;
            gameManager.mapGenaretor.bombCount = level.bombCount;
            gameManager.scoreManager.score = level.score;

            gameManager.mapGenaretor.GenerateEmptyMap();
            var mines = gameManager.mapGenaretor.MineDatas;
            for (int w = 0; w < level.with; w++)
            {
                for (int h = 0; h < level.hight; h++)
                {
                    MineData mineData = mines[w, h];
                    mineData.LoadMineDataSave(level.level[w, h]);
                }
            }
        }
        public void SaveLevel()
        {
            levels.Add(new Level(GameManager.Instance()));
            SaveSystem.Instance().SaveSaveFile();
        }
    }
}
