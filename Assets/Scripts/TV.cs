using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TV : MonoBehaviour
{
    private bool isWorking = false;

    [Header("Sprites")]
    [SerializeField]
    private Sprite enableSprite;
    [SerializeField]
    private Sprite disableSprite;

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
            GetComponent<Image>().sprite = enableSprite;
        }
        else
        {
            GetComponent<Image>().sprite = disableSprite;
        }
    }

    private void TurnOnOrOff()
    {
        isWorking = !isWorking;
        SetSprite();

        StartCoroutine(Enjoying());
    }
    
    private IEnumerator Enjoying()
    {   
        while(isWorking)
        {   
            yield return new WaitForSeconds(.5f);
            hero.GetComponent<Player>().AddFunny(1);
        }
    }
}
