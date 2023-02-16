using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The offset from the border in which the camera will start moving")]
    private float edgeSize;
    [SerializeField]
    [Tooltip("The speed in which the camera moves")]
    private float cameraSpeed;
    [SerializeField]
    [Tooltip("Locks the camera movement")]
    private bool cameraLocked;

    [Header("Constraints")]
    [SerializeField]
    private bool _x;
    [SerializeField]
    private bool _y;

    [Header("Playing field")]
    [SerializeField]
    [Tooltip("Shows the area in which the player can move the camera")]
    private Vector2 size;
    [SerializeField]
    [Tooltip("Visualises the playing field")]
    private bool drawGizmos;

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(size.x / 2);
        if (!cameraLocked)
        {
            if (!_x)
            {
                if (!(transform.position.x > size.x / 2))
                {
                    if (Input.mousePosition.x > Screen.width - edgeSize)
                    {
                        transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0);
                    }
                }
                if (!(transform.position.x < -size.x / 2))
                {
                    if (Input.mousePosition.x < edgeSize)
                    {
                        transform.position -= new Vector3(cameraSpeed * Time.deltaTime, 0);
                    }
                }
            }

            if (!_y)
            {
                if (!(transform.position.y > size.y / 2))
                {
                    if (Input.mousePosition.y > Screen.height - edgeSize)
                    {
                        transform.position += new Vector3(0, cameraSpeed * Time.deltaTime);
                    }
                }
                if (!(transform.position.y < -size.y / 2))
                {
                    if (Input.mousePosition.y < edgeSize)
                    {
                        transform.position -= new Vector3(0, cameraSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = new Color(1, 1, 1, 0.2f);
            Gizmos.DrawCube(new Vector3(0, 0, 15), size);

        }
    }
}
