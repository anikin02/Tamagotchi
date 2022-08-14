using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwapScene : MonoBehaviour
{
    public string NextScene;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {   
        SceneManager.LoadScene(NextScene);
    }   
}
