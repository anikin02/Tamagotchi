using UnityEngine;
using System.IO;
using System;

public class SaveGame : MonoBehaviour
{
    private Save save = new Save();
    private string path;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        path = Path.Combine(Application.persistentDataPath, "Save.json");
    }

    // Method for saving all parametrs.
    private void SaveAll()
    {
        save.Age = DataHolder.Age;
        save.Race = DataHolder.Race;

        save.AgePoints = DataHolder.AgePoints;
        save.HungerPoints = DataHolder.HungerPoints;
        save.FunnyPoints = DataHolder.FunnyPoints;
        save.CleanPoints = DataHolder.CleanPoints;
        save.SleepPoints = DataHolder.SleepPoints;

        save.ToSlepping = DataHolder.ToSlepping;

        DateTime pastDate = new DateTime();
        pastDate = DateTime.Now;
        save.pastDate = pastDate.ToString();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {   
            SaveAll();
            File.WriteAllText(path, JsonUtility.ToJson(save));
        }   
    }
}

// Class of what we need to save.
[Serializable]
public class Save
{
    public byte Race;
    public byte Age;

    public int AgePoints;
    public byte HungerPoints;
    public byte SleepPoints;
    public byte CleanPoints;
    public byte FunnyPoints;
    public bool ToSlepping;

    public string pastDate;
}