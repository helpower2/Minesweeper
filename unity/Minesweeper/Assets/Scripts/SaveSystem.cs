using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

//this is for saving the level
//I(Jeffrey) have made this becuase i'm bored

namespace Saving
{
    public class SaveSystem : Singleton<SaveSystem>
    {
        public Dropdown dropdown;
        public Text currentlevelText;
        public SaveFile save = new SaveFile();
        public SaveFile.Level currentLevel;
        public int currentLevelint;
        private string dataPath;
        // Start is called before the first frame update
        IEnumerator Start()
        {
            dataPath = Application.dataPath + "\\save.bin";
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
                    if (save.levels.Count != 0)
                    {
                        currentLevel = save.levels[save.levels.Count - 1];
                        LoadLevel();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                throw;
            }
            updateGraphics();
        }

        private void CreateSaveFile()
        {
            FileStream fileStream = File.Create(dataPath);
            //using (StreamWriter streamWriter = new StreamWriter(fileStream)){streamWriter.WriteLine(JsonUtility.ToJson(save));}
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, save);
            fileStream.Close();
        }

        public void LoadSaveFile()
        {
            FileStream fileStream = File.OpenRead(dataPath);
            BinaryFormatter formatter = new BinaryFormatter();
            save = (SaveFile)formatter.Deserialize(fileStream);
            fileStream.Close();

            //save = JsonUtility.FromJson<SaveFile>(File.ReadLines(dataPath).ToString());
            updateGraphics();
        }
        public void SaveSaveFile()
        {
            FileStream fileStream = File.OpenWrite(dataPath);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, save);
            fileStream.Close();
            updateGraphics();
        }
        public void SaveCurrentLevel()
        {
            save.SaveLevel();
            updateGraphics();
            dropdown.SetValueWithoutNotify(save.levels.Count);
        }
        public void SetcurrentLevel(int level)
        {
            if (save.levels.Count <= level) return;
            currentLevel = save.levels[level];
            currentlevelText.text = currentLevel.ToString();
            dropdown.SetValueWithoutNotify(level);
        }
        public void LoadLevel()
        {
            save.loadlevel(currentLevel);
            updateGraphics();
            
        }
        public void updateGraphics()
        {
            dropdown.ClearOptions();
            List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
            foreach (var item in save.levels)
            {
                optionDatas.Add(new Dropdown.OptionData(item.ToString()));
            }
            dropdown.AddOptions(optionDatas);
            dropdown.RefreshShownValue();
        }
        public void DeleteLevel()
        {
            save.levels.Remove(currentLevel);
            currentLevel = new SaveFile.Level();
            updateGraphics();
            SaveSaveFile();
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

            //public Vector2Int localPos;
            public int localPosW;
            public int localPosH;
            public MineDataSave(MineData mineData)
            {
                isBomb = mineData.isBomb;
                totalbombsNearby = mineData.totalbombsNearby;
                isRevealed = mineData.isRevealed;
                localPosW = mineData.localPos.x;
                localPosH = mineData.localPos.y;
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
            public MineDataSave[] level;
            public DateTime saveTime;
            public int score;
            public float time;

            public Level(GameManager gameManager)
            {
                this.levelName = gameManager.levelName;
                with = gameManager.mapGenaretor.width;
                hight = gameManager.mapGenaretor.heigth;
                bombCount = gameManager.mapGenaretor.bombCount;
                level = new MineDataSave[gameManager.mapGenaretor.width * gameManager.mapGenaretor.heigth];
                for (int w = 0; w < gameManager.mapGenaretor.width; w++)
                {
                    for (int h = 0; h < gameManager.mapGenaretor.heigth; h++)
                    {
                        level[gameManager.mapGenaretor.heigth * h + w] = gameManager.mapGenaretor.MineDatas[h, w];
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
            public override string ToString()
            {
                return levelName + ", Score " + score;
            }

        }
        public List<Level> levels;

        public void loadlevel(Level level)
        {
            GameManager gameManager = GameManager.Instance();
            gameManager.levelName = level.levelName;
            gameManager.mapGenaretor.width = level.with;
            gameManager.mapGenaretor.heigth = level.hight;
            gameManager.mapGenaretor.bombCount = level.bombCount;
            gameManager.scoreManager.score = level.score;

            gameManager.mapGenaretor.GenerateEmptyMap();
            var mines = gameManager.mapGenaretor.MineDatas;
            for (int w = 0; w < level.with; w++)
            {
                for (int h = 0; h < level.hight; h++)
                {
                    MineData mineData = mines[w, h];
                    mineData.LoadMineDataSave(level.level[level.with * w + h]);
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
