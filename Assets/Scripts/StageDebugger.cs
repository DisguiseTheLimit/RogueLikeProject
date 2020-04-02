using UnityEngine;

public class StageDebugger : MonoBehaviour
{
    void OnEnable()
    {
        PlayerController player = StageManager.Instance.Player;

        player.HealthChanged += PlayerHealthChanged;
        player.Killed += PlayerKilled;
    }

    void OnDisable()
    {
        PlayerController player = StageManager.Instance.Player;

        player.HealthChanged -= PlayerHealthChanged;
        player.Killed -= PlayerKilled;
    }

    void PlayerHealthChanged(PlayerController controller)
    {
        Debug.Log($"{controller.name} health changed to {controller.Health}");
    }

    void PlayerKilled(PlayerController controller)
    {
        Debug.Log($"{controller.name} killed");
    }
}