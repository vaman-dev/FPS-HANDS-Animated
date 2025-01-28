using UnityEngine;

public class GunController : MonoBehaviour
{
    public Animator animator; // Assign in Inspector
    public Camera playerCamera;
    
    void Update()
    {
        // Right-click to aim
        if (Input.GetMouseButton(1)) // Right Mouse Button
        {
            animator.SetBool("isAiming", true);
        }
        else
        {
            animator.SetBool("isAiming", false);
        }

        // Left-click to shoot (Only if aiming)
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Shoot", true);
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
    }
}
