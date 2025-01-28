using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EvolveGames
{
    public class HandsHolder : MonoBehaviour
    {
        [Header("Hands Holder Settings")]
        [SerializeField] private bool enabledBobbing = true;
        [SerializeField] private bool enabledSway = true;
        [SerializeField] private bool enabledBreathing = true;

        [Header("Bobbing Motion")]
        [SerializeField, Range(0.0005f, 0.02f)] private float bobAmount = 0.008f;
        [SerializeField, Range(1.0f, 3.0f)] private float sprintMultiplier = 1.6f;
        [SerializeField, Range(5f, 20f)] private float bobFrequency = 12.0f;
        [SerializeField, Range(50f, 10f)] private float smoothness = 18f;

        [Header("Sway Motion")]
        [SerializeField, Range(0.1f, 10.0f)] private float swayMultiplier = 4f;

        [Header("Breathing Motion")]
        [SerializeField, Range(0.0001f, 0.005f)] private float breathAmount = 0.002f;
        [SerializeField, Range(1.0f, 5.0f)] private float breathSpeed = 1.5f;

        [Header("Footstep Audio")]
        public AudioSource footstepAudio; // Assign in Inspector
        public AudioClip[] footstepSounds; // Assign different footstep sounds in the Inspector
        [SerializeField, Range(0.2f, 1f)] private float footstepInterval = 0.5f; // Time between steps

        private Vector3 originalPosition;
        private Quaternion originalRotation;
        private Vector3 targetPosition;
        private Quaternion targetRotation;
        private CharacterController player;
        private float footstepTimer;

        private void Awake()
        {
            player = GetComponentInParent<CharacterController>();
            originalPosition = transform.localPosition;
            originalRotation = transform.localRotation;
        }

        private void Update()
        {
            if (!enabledBobbing && !enabledSway && !enabledBreathing) return;

            float speed = new Vector3(player.velocity.x, 0, player.velocity.z).magnitude;
            float movementFactor = Mathf.Clamp01(speed / 5.0f);

            ApplyBobbing(movementFactor);
            ApplySway();
            ApplyBreathing();
            HandleFootsteps(speed);

            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, smoothness * Time.deltaTime);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, smoothness * Time.deltaTime);
        }

        private void ApplyBobbing(float movementFactor)
        {
            if (!enabledBobbing) return;

            float sprintEffect = (Input.GetKey(KeyCode.LeftShift)) ? sprintMultiplier : 1.0f;
            float bobbingEffect = Mathf.Sin(Time.time * bobFrequency * sprintEffect) * bobAmount * movementFactor;
            float lateralEffect = Mathf.Cos(Time.time * bobFrequency / 2f) * bobAmount * 0.5f * movementFactor;

            targetPosition = originalPosition + new Vector3(lateralEffect, bobbingEffect, 0);
        }

        private void ApplySway()
        {
            if (!enabledSway) return;

            float mouseX = Input.GetAxis("Mouse X") * swayMultiplier * -0.02f;
            float mouseY = Input.GetAxis("Mouse Y") * swayMultiplier * -0.02f;

            targetRotation = Quaternion.Euler(originalRotation.eulerAngles + new Vector3(mouseY, mouseX, mouseX * 2f));
        }

        private void ApplyBreathing()
        {
            if (!enabledBreathing) return;

            float breathEffect = Mathf.Sin(Time.time * breathSpeed) * breathAmount;
            targetPosition += new Vector3(0, breathEffect, 0);
        }

        private void HandleFootsteps(float speed)
        {
            if (footstepAudio == null || footstepSounds.Length == 0 || speed < 1f || !player.isGrounded) return;

            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f)
            {
                footstepAudio.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)]);
                footstepTimer = footstepInterval / (speed * 0.5f); // Adjust timing based on speed
            }
        }
    }
}
