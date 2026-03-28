using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public Vector2 offset = new Vector2(3f, 1f);

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 target = new Vector3(
            player.position.x + offset.x,
            player.position.y + offset.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position, target, smoothSpeed * Time.deltaTime
        );
    }
}