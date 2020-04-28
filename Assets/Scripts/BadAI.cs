using UnityEngine;

public class BadAI : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    ProjectileShooter projectileShooter;

    [SerializeField]
    float frequency = 0.01f;

    private void Update()
    {
        Vector2 moveInput = new Vector2(Mathf.Sin(Time.time * frequency),0);
        playerController.Move(moveInput);

       
            projectileShooter.TryShoot();
        

    }
}
