using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{   
    [SerializeField]
    private GameObject player;
    [SerializeField]
    public byte foodPoints = 0;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(eatFood);
    }

    private void eatFood()
    {
        player.GetComponent<Player>().AddHunger(foodPoints);
    }
}
