using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shower : MonoBehaviour
{
    private bool isWorking = false;

    [SerializeField]
    private GameObject hero;
    [SerializeField]
    private GameObject water;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(TurnOnOrOff);
    }

    private void TurnOnOrOff()
    {
        isWorking = !isWorking;
        
        if (isWorking)
        {
            water.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            water.GetComponent<ParticleSystem>().Stop();
        }

        StartCoroutine(Washing());
    }
    
    private IEnumerator Washing()
    {   
        while(isWorking)
        {   
            yield return new WaitForSeconds(.6f);
            hero.GetComponent<Player>().AddClean(1);
        }
    }
}
