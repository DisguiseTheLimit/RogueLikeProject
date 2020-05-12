using UnityEngine;
// This script controls the movement of the player character

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    ProjectileShooter projectileShooter;

    bool weaponSwitching = true;

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
        playerController.Move(moveInput);

        if (Input.GetKey(KeyCode.Alpha2))
        {
            weaponSwitching = false;
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            weaponSwitching = true;
        }

        if (Input.GetMouseButton(0) && weaponSwitching)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (ProjectileShooter.ammoCount > 0)
            {
                projectileShooter.TryShoot(mousePosition);
                projectileShooter.AmmoCount();
            }
            //if (ProjectileShooter.ammoCount == 0)
            //{
                //projectileShooter.Reload();
            //}
        }
    }
}