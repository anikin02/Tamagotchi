using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{   
    [Header("Game Objects")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject hungryBar;
    [SerializeField]
    private GameObject cleanBar;
    [SerializeField]
    private GameObject funnyBar;
    [SerializeField]
    private GameObject sleepBar;

    [Space(10)]
    [Header("Sprites")]
    [SerializeField]
    private Sprite[] spritesProcent = new Sprite[8];

    private void FixedUpdate()
    {
        ChangeBar(hungryBar, player.GetComponent<Player>().HungerPoints);
        ChangeBar(sleepBar, player.GetComponent<Player>().SleepPoints);
        ChangeBar(funnyBar, player.GetComponent<Player>().FunnyPoints);
        ChangeBar(cleanBar, player.GetComponent<Player>().CleanPoints);        
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

}
