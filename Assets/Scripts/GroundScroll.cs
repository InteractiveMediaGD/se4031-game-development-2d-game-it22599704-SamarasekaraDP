using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    public Transform ground1;
    public Transform ground2;
    public float scrollSpeed = 3f;
    private float groundWidth = 30f;

    void Update()
    {
        if (ground1 == null || ground2 == null) return;

        ground1.position += Vector3.left * scrollSpeed * Time.deltaTime;
        ground2.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (ground1.position.x <= -groundWidth)
            ground1.position = new Vector3(
                ground2.position.x + groundWidth,
                ground1.position.y, 0);

        if (ground2.position.x <= -groundWidth)
            ground2.position = new Vector3(
                ground1.position.x + groundWidth,
                ground2.position.y, 0);
    }

    public void UpdateSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }
}