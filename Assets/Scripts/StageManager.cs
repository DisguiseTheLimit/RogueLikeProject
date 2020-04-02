using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public PlayerController Player => player;

    [SerializeField]
    PlayerController player;

    void Awake()
    {
        Instance = this;
    }
}
