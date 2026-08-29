using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerDoorPush : MonoBehaviour
{
    public float pushStrength = 10.95f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        DoorScript.Door door =
            hit.collider.GetComponentInParent<DoorScript.Door>();

        if (door == null)
            return;

        if (!door.unlocked)
            return;

        Vector3 playerMove = hit.moveDirection;

        Vector3 directionToPlayer =
            transform.position - door.transform.position;

        float side = Vector3.Dot(
            door.transform.up,
            Vector3.Cross(
                directionToPlayer,
                playerMove
            )
        );

        float pushAmount =
            Mathf.Sign(side) * pushStrength;

        door.PushDoor(pushAmount);
    }
}