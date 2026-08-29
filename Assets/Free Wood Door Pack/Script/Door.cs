using UnityEngine;

namespace DoorScript
{
    public class Door : MonoBehaviour
    {
        [Header("Door State")]
        public bool unlocked = false;
        public bool playerNear = false;

        [Header("Door Settings")]
        public float openAngle = -15f;
        public float closedAngle = 0f;
        public float pushSpeed = 50f;

        [Header("Auto Close")]
        public bool autoClose = false;
        public float autoCloseDelay = 5f;

        private float currentAngle = 0f;
        private float targetAngle = 0f;

        private float autoCloseTimer = 0f;

        private void Update()
        {
            currentAngle = Mathf.MoveTowards(
                currentAngle,
                targetAngle,
                pushSpeed * Time.deltaTime
            );

            transform.localRotation =
                Quaternion.Euler(0f, currentAngle, 0f);

            // AUTO CLOSE
            if (autoClose && unlocked && targetAngle != closedAngle)
            {
                autoCloseTimer -= Time.deltaTime;

                if (autoCloseTimer <= 0f)
                {
                    CloseDoor();
                }
            }
        }

        public void UnlockDoor()
        {
            unlocked = true;

            // Pintu tetap tertutup sampai player mendorongnya.
            targetAngle = currentAngle;

            // Reset timer auto-close
            autoCloseTimer = autoCloseDelay;
        }

        public void OpenDoor()
        {
            UnlockDoor();
        }

        public void CloseDoor()
        {
            unlocked = false;
            targetAngle = closedAngle;
        }

        public void PushDoor(float amount)
        {
            if (!unlocked)
                return;

            targetAngle += amount;

            targetAngle = Mathf.Clamp(
                targetAngle,
                -90f,
                90f
            );

            // Reset auto-close timer setiap kali didorong
            autoCloseTimer = autoCloseDelay;
        }
    }
}