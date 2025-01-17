// using UnityEngine;

// public class HandgunAnimationController : MonoBehaviour
// {
//     private Animator animator;

//     void Start()
//     {
//         // Get the Animator component attached to this GameObject
//         animator = GetComponent<Animator>();
//         if (animator == null)
//         {
//             Debug.LogError("Animator component not found on " + gameObject.name);
//         }
//     }

//     void Update()
//     {
//         // Check if any movement key is pressed 
//         if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || 
//             Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) ||
//             Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || 
//             Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
//         {
//             // Trigger walk animation
//             animator.SetBool("IsWalking", true);
//         }
//         else
//         {
//             // Stop walk animation
//             animator.SetBool("IsWalking", false);
//         }
//     }
// }
