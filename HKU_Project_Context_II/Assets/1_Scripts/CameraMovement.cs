using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    [Tooltip("The offset from the border in which the camera will start moving")]
    private float edgeSize;
    [SerializeField]
    [Tooltip("The speed in which the camera moves")]
    private float cameraSpeed;
    [SerializeField]
    [Tooltip("Locks the camera movement")]
    public bool cameraLocked;

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

    [Header("Targets")]
    [SerializeField]
    private Transform desktopPos;
    [SerializeField]
    private Transform deskPos;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!cameraLocked)
        {
            if (!_x)
            {
                if (!(cam.transform.position.x > size.x / 2))
                {
                    if (Input.mousePosition.x > Screen.width - edgeSize)
                    {
                        cam.transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0);
                    }
                }
                if (!(cam.transform.position.x < -size.x / 2))
                {
                    if (Input.mousePosition.x < edgeSize)
                    {
                        cam.transform.position -= new Vector3(cameraSpeed * Time.deltaTime, 0);
                    }
                }
            }

            if (!_y)
            {
                if (!(cam.transform.position.y > size.y / 2))
                {
                    if (Input.mousePosition.y > Screen.height - edgeSize)
                    {
                        cam.transform.position += new Vector3(0, cameraSpeed * Time.deltaTime);
                    }
                }
                if (!(cam.transform.position.y < -size.y / 2))
                {
                    if (Input.mousePosition.y < edgeSize)
                    {
                        cam.transform.position -= new Vector3(0, cameraSpeed * Time.deltaTime);
                    }
                }
            }
        }
    }

    public IEnumerator MoveToDesktop()
    {
        float elapsedTime = 0;
        while (elapsedTime < 3)
        {
            cam.transform.position = Vector2.Lerp(cam.transform.position, desktopPos.position, (elapsedTime / 3));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        // Make sure we got there
        cam.transform.position = desktopPos.position;
        yield return null;
    }

    public IEnumerator MoveToDesk()
    {
        float elapsedTime = 0;
        while (elapsedTime < 5)
        {
            cam.transform.position = Vector2.Lerp(cam.transform.position, deskPos.position, (elapsedTime / 5));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        // Make sure we got there
        cam.transform.position = deskPos.position;
        cameraLocked = false;
        yield return null;
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
