using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public PlayerController Player { get; set; }


    void Awake()
    {
        Instance = this;
    }
}
