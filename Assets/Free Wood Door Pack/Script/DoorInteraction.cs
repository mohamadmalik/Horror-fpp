using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteraction : MonoBehaviour
{
    public DoorScript.Door door;

    private Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.playerNear = true;
            playerTransform = other.transform.root;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.playerNear = false;
            playerTransform = null;
        }
    }

    private static int lastInteractionFrame = -1;

    private void Update()
    {
        if (!door.playerNear || lastInteractionFrame == Time.frameCount)
            return;

        bool pressedE = Keyboard.current != null &&
                        Keyboard.current.eKey.wasPressedThisFrame;

        if (pressedE)
        {
            lastInteractionFrame = Time.frameCount;
            if (!door.unlocked)
            {
                door.UnlockDoor();

                if (playerTransform != null)
                {
                    Vector3 directionToPlayer =
                        playerTransform.position - door.transform.position;
                    float side = Vector3.Dot(
                        door.transform.up,
                        Vector3.Cross(directionToPlayer, playerTransform.forward)
                    );
                    door.PushDoor(Mathf.Sign(side) * 20f);
                }
            }
            else
                door.CloseDoor();
        }
    }
}