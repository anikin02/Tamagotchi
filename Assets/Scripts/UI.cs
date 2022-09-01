using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{   
    [Header("Player")]
    [SerializeField]
    private GameObject player;

    [Header("Bars")]
    [SerializeField]
    private GameObject hungryBar;
    [SerializeField]
    private GameObject cleanBar;
    [SerializeField]
    private GameObject funnyBar;
    [SerializeField]
    private GameObject sleepBar;

    [Header("Icons")]
    //0 - hungry; 1 - clean; 2 - funny; 3 - sleep;
    [SerializeField]
    private GameObject[] Icons = new GameObject[4];

    [Space(10)]
    [Header("Sprites")]
    [SerializeField]
    private Sprite[] spritesProcent = new Sprite[8];

    // Second: 0 - hungry; 1 - clean; 2 - funny; 3 - sleep;
    [SerializeField]
    private Sprite[] iconNormalSprites = new Sprite[4];
    [SerializeField]
    private Sprite[] iconRedSprites = new Sprite[4]; 

    private void Update()
    {
        ChangeBar(hungryBar, player.GetComponent<Player>().HungerPoints);
        ChangeIcon(0, player.GetComponent<Player>().HungerPoints);
        ChangeBar(sleepBar, player.GetComponent<Player>().SleepPoints);
        ChangeIcon(3, player.GetComponent<Player>().SleepPoints);
        ChangeBar(funnyBar, player.GetComponent<Player>().FunnyPoints);
        ChangeIcon(2, player.GetComponent<Player>().FunnyPoints);
        ChangeBar(cleanBar, player.GetComponent<Player>().CleanPoints);
        ChangeIcon(1, player.GetComponent<Player>().CleanPoints);
    }

    private void ChangeBar(GameObject bar, byte procent)
    {
        if (procent > 88)
        {
            bar.GetComponent<Image>().sprite = spritesProcent[7];
        }
        else if (procent > 76)
        {
            bar.GetComponent<Image>().sprite = spritesProcent[6];
        }
        else if (procent > 64)
        {
            bar.GetComponent<Image>().sprite = spritesProcent[5];
        }
        else if (procent > 52)
        {
            bar.GetComponent<Image>().sprite = spritesProcent[4];
        }
        else if (procent > 35)
        {
            bar.GetComponent<Image>().sprite = spritesProcent[3];
        }
        else if (procent > 23)
        {
            bar.GetComponent<Image>().sprite = spritesProcent[2];
        }
        else if (procent > 11)
        {
            bar.GetComponent<Image>().sprite = spritesProcent[1];
        }
        else
        {
            bar.GetComponent<Image>().sprite = spritesProcent[0];         
        }
    }

    private void ChangeIcon(byte numberIcon, byte procent)
    {
        if (procent < 10)
        {
            Icons[numberIcon].GetComponent<Image>().sprite = iconRedSprites[numberIcon];
        }
        else
        {
            Icons[numberIcon].GetComponent<Image>().sprite = iconNormalSprites[numberIcon];
        }
    }
}
