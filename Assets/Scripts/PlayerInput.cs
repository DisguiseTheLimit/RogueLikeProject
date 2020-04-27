using UnityEngine;
// This script controls the movement of the player character

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    ProjectileShooter projectileShooter;

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
        playerController.Move(moveInput);
        
        if (Input.GetMouseButton(0))
        {
            projectileShooter.TryShoot();
        }
    }
}