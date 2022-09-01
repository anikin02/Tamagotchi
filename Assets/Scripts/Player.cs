using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

public class Player : MonoBehaviour
{
    private Save save = new Save();
    
    // 0 - baby; 5 - child; 10 - teen; 15 - adult; 20 - old man; 25 - ghost.
    public byte Age = 0;
    public byte Race = 0;
    public int AgePoints = 0;

    // 0 - normal; 1 - happy; 2 - dirty; 3 - slepper; 4 - dirty slepper.
    private byte condition = 0;

    [SerializeField]
    private Sprite[] spritesConditions = new Sprite[26];

    // Hero properties.
    public byte HungerPoints = 50;
    public byte SleepPoints = 50;
    public byte CleanPoints = 50;
    public byte FunnyPoints = 50;
    public bool ToSlepping = false;

    private void Awake()
    {   
        string path;
        path = Path.Combine(Application.persistentDataPath, "Save.json");

        if (File.Exists(path) && DataHolder.BackToTheFuture)
        {   
            DataHolder.BackToTheFuture = false;
            save = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            
            DataHolder.Race = save.Race;
            offlineGame();
        }
    }

    private void Start()
    {
        setFields();
        changeCondition();

        StartCoroutine(Starve());
        StartCoroutine(Wakefulness());
        StartCoroutine(Yearn());
        StartCoroutine(Dirty());
        StartCoroutine(Aging());
    }

    private void FixedUpdate()
    {
        changeAge();
        changeCondition();
        saveFields();
    }

    private void OnDestroy()
    {
        saveFields();
    }
    
    private void offlineGame()
    {
        DateTime newDate = new DateTime();
        newDate = DateTime.Now;
        int different = (int)(newDate.Subtract(DateTime.Parse(save.pastDate))).TotalSeconds;

        if (save.FunnyPoints > Convert.ToByte(different/576))
        {
            FunnyPoints = Convert.ToByte(save.FunnyPoints - Convert.ToByte(different/576));
        }
        else
        {
            FunnyPoints = 0;
        }
        if (save.CleanPoints > (Convert.ToByte(different/432)))
        {
            CleanPoints = Convert.ToByte(save.CleanPoints - (Convert.ToByte(different/432)));
        }
        else
        {
            CleanPoints = 0;
        }
        if (save.HungerPoints > (Convert.ToByte(different/114)))
        {
            HungerPoints = Convert.ToByte(save.HungerPoints - (Convert.ToByte(different/114)));
        }
        else
        {
            HungerPoints = 0;
        }
        AgePoints = save.AgePoints + different;

        if (save.ToSlepping)
        {
            if (Convert.ToByte(different/180) + save.SleepPoints > 100)
            {
                SleepPoints = 100;
            }
            else
            {
                SleepPoints = Convert.ToByte(Convert.ToByte(different/180) + save.SleepPoints);
            }
        }
        else
        {
            if (Convert.ToByte(different/576) > save.SleepPoints)
            {
                SleepPoints = 0;
            }
            else
            {   
                SleepPoints = Convert.ToByte(save.SleepPoints - Convert.ToByte(different/576));
            }
        }
    }

    private void setFields()
    {   
        if (DataHolder.Age != 111)
        {
            Age = DataHolder.Age;
            Race = DataHolder.Race;
            condition = DataHolder.Condition;
            HungerPoints = DataHolder.HungerPoints;
            SleepPoints = DataHolder.SleepPoints;
            CleanPoints = DataHolder.CleanPoints;
            FunnyPoints = DataHolder.FunnyPoints;
        }
    }

    private void saveFields()
    {   
        DataHolder.Age = Age;
        DataHolder.Race = Race;
        DataHolder.Condition = condition;
        DataHolder.HungerPoints = HungerPoints;
        DataHolder.SleepPoints = SleepPoints;
        DataHolder.CleanPoints = CleanPoints;
        DataHolder.FunnyPoints = FunnyPoints;
        DataHolder.AgePoints = AgePoints;
    }

    // 100 points in 4 hours.
    private IEnumerator Starve()
    {   
        while(true)
        {
            yield return new WaitForSeconds(144.0f);
            SubtractHunger(1);
        }
    }

    // 100 points in 1 minute.
    private IEnumerator Wash()
    {   
        while(true)
        {
            yield return new WaitForSeconds(0.6f);
            AddClean(1);
        }
    }

    // 100 points in 12 hours.
    private IEnumerator Dirty()
    {   
        while(true)
        {
            yield return new WaitForSeconds(432.0f);
            SubtractClean(1);
        }
    }

    // 100 points in 16 hours
    private IEnumerator Wakefulness()
    {   
        while(true)
        {
            yield return new WaitForSeconds(576.0f);
            SubtractSleep(1);
        }
    }

    // 100 points in 6 hours.
    private IEnumerator Yearn()
    {   
        while(true)
        {
            yield return new WaitForSeconds(216.0f);
            SubtractFunny(1);
        }
    }

    // baby - 0/3600 points (24 hour); child - 3600/10800 points; teen - 10800/21600 points;
    // adult - 21600/32400 points; old man - 32400/39600 points; ghost - 39600 points. 
    private IEnumerator Aging()
    {   
        while(true)
        {
            yield return new WaitForSeconds(1.0f);
            AddAge(1);
        }
    }

    private void changeAge()
    {
        if (AgePoints > 39600)
        {
            Age = 20; // 25
        }
        if (AgePoints > 32400)
        {
            Age = 20;
        }
        if (AgePoints > 21600)
        {
            Age = 15;
        }
        if (AgePoints > 10800)
        {
            Age = 10;
        }
        if (AgePoints > 3600)
        {
            Age = 5;
        }
        else
        {
            Age = 0;
        }
    }

    private void changeCondition()
    {
        if (ToSlepping)
        {
            if (CleanPoints < 20)
            {
                condition = 4;
            }
            else
            {
                condition = 3;
            }
        }
        else if ((HungerPoints > 80) && (SleepPoints > 80) && (FunnyPoints > 80) && (CleanPoints > 80))
        {
            condition = 1;
        }
        else if (CleanPoints < 20)
        {
            condition = 2;
        }
        else
        {
            condition = 0;
        }

        changeSprite();
    }

    private void changeSprite()
    {
        print(Age);
        print(condition);
        GetComponent<SpriteRenderer>().sprite = spritesConditions[(Age + condition)];
    }

    public void AddAge(int quantity)
    {
        AgePoints += quantity;
    }

    public void AddHunger(byte quantity)
    {   
        if (HungerPoints + quantity < 100)
        {
            HungerPoints += quantity;
        }
        else if (HungerPoints == 100)
        {
            SubtractClean(5);
        }
        else
        {
            HungerPoints = 100;
        }
        
    }

    public void SubtractHunger(byte quantity)
    {
        if (HungerPoints > quantity)
        {
            HungerPoints -= quantity;
        }
        else
        {
            HungerPoints = 0;
        }
    }

    public void AddSleep(byte quantity)
    {
        if (SleepPoints + quantity < 100)
        {
            SleepPoints += quantity;
        }
        else
        {
            SleepPoints = 100;
        }
    }

    public void SubtractSleep(byte quantity)
    {
        if (SleepPoints > quantity)
        {
            SleepPoints -= quantity;
        }
        else
        {
            SleepPoints = 0;
        }
    }

    public void AddClean(byte quantity)
    {
        if (CleanPoints + quantity < 100)
        {
            CleanPoints += quantity;
        }
        else
        {
            CleanPoints = 100;
        }
    }

    public void SubtractClean(byte quantity)
    {
        if (CleanPoints > quantity)
        {
            CleanPoints -= quantity;
        }
        else
        {
            CleanPoints = 0;
        }
    }

    public void AddFunny(byte quantity)
    {
        if (FunnyPoints + quantity < 100)
        {
            FunnyPoints += quantity;
        }
        else
        {
            FunnyPoints = 100;
        }
    }

    public void SubtractFunny(byte quantity)
    {   
        if (FunnyPoints > quantity)
        {
            FunnyPoints -= quantity;
        }
        else
        {
            FunnyPoints = 0;
        }
    }

    private void death()
    {
        // Change sprite; all fields is 0; cant eat, take shower and watch tv; message about a death and new game.    
    }
}