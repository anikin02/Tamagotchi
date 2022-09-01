using UnityEngine;
using UnityEngine.UI;

public class Fridge : MonoBehaviour
{   
    [SerializeField]
    private GameObject foods;

    private bool enable = false;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(openOrClose);
    }

    private void openOrClose()
    {   
        enable = !enable;
        foods.SetActive(enable);
    }
}
