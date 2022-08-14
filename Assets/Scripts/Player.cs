using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour
{   
    private byte age = 0;
    private byte race = 0;
    private byte condition = 0;

    private Sprite[,] spritesConditions = new Sprite[5, 22];

    // Hero properties.
    public byte HungerPoints = 50;
    public byte SleepPoints = 50;
    public byte CleanPoints = 50;
    public byte FunnyPoints = 50;

    private void Start()
    {
        setFields();

        StartCoroutine(Starve());
        StartCoroutine(Wakefulness());
        StartCoroutine(Yearn());
        StartCoroutine(Dirty());
    }

    private void OnDestroy()
    {
        saveFields();
    }

    private void setFields()
    {   
        if (DataHolder.Age != 111)
        {
            age = DataHolder.Age;
            race = DataHolder.Race;
            condition = DataHolder.Condition;
            HungerPoints = DataHolder.HungerPoints;
            SleepPoints = DataHolder.SleepPoints;
            CleanPoints = DataHolder.CleanPoints;
            FunnyPoints = DataHolder.FunnyPoints;
        }
    }

    private void saveFields()
    {   
        DataHolder.Age = age;
        DataHolder.Race = race;
        DataHolder.Condition = condition;
        DataHolder.HungerPoints = HungerPoints;
        DataHolder.SleepPoints = SleepPoints;
        DataHolder.CleanPoints = CleanPoints;
        DataHolder.FunnyPoints = FunnyPoints;
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

    private void choiceSprite()
    {
        //sprite = spriteArr[race, age + condition]
    }

    public void AddHunger(byte quantity)
    {   
        if (HungerPoints + quantity < 100)
        {
            HungerPoints += quantity;
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
