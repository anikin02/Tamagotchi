using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Lamp : MonoBehaviour
{
    private bool isWorking = true;

    [Header("Sprites")]
    [SerializeField]
    private Sprite enableSpite;
    [SerializeField]
    private Sprite disableSpite;

    [Space(10)]
    [Header("Hero")]
    [SerializeField]
    private GameObject hero;

    void Start()
    {
        SetSprite();

        GetComponent<Button>().onClick.AddListener(TurnOnOrOff);
    }

    
    private void SetSprite()
    {
        if (isWorking)
        {
            GetComponent<Image>().sprite = enableSpite;
        }
        else
        {
            GetComponent<Image>().sprite = disableSpite;
        }
    }

    private void TurnOnOrOff()
    {
        isWorking = !isWorking;
        SetSprite();
        StartCoroutine(Sleeping());
    }

    // 100 points in 5 hours.
    private IEnumerator Sleeping()
    {   
        while(isWorking)
        {   
            yield return new WaitForSeconds(180.0f);
            hero.GetComponent<Player>().AddSleep(1);
        }
    }
}
