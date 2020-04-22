using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public HealthController Player => player;

    [SerializeField]
    HealthController player;

    void Awake()
    {
        Instance = this;
    }
}
