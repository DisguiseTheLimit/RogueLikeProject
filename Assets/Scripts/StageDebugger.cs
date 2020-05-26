using UnityEngine;

public class StageDebugger : MonoBehaviour
{
    void OnEnable()
    {
        HealthController player = StageManager.Instance.Player.healthController;

        player.HealthChanged += PlayerHealthChanged;
        player.Killed += PlayerKilled;
    }

    void OnDisable()
    {
        HealthController player = StageManager.Instance.Player.healthController;

        player.HealthChanged -= PlayerHealthChanged;
        player.Killed -= PlayerKilled;
    }

    void PlayerHealthChanged(HealthController controller)
    {
        Debug.Log($"{controller.name} health changed to {controller.Health}");
    }

    void PlayerKilled(HealthController controller)
    {
        Debug.Log($"{controller.name} killed");
    }
}