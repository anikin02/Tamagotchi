using UnityEngine;

public class AlwaysLife : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
