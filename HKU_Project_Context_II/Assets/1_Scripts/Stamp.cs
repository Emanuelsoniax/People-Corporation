using UnityEngine;

public enum StampType { Decline, Approve }
public class Stamp : MonoBehaviour
{
    [Header("Stamp settings")]
    [SerializeField]
    private StampType stampType;
    [Tooltip("The distance in which a Stampable will be able to be stamped")]
    public float snapDistanceX;
    [Tooltip("The distance in which a Stampable will be able to be stamped")]
    public float snapDistanceY;

    [Header("Audio")]
    [SerializeField]
    [Tooltip("The AudioSource that plays the Dragable SFX")]
    private AudioSource audioSource;
    [SerializeField]
    [Tooltip("The AudioClip that's played when the Dragable is picked up")]
    private AudioClip pickup;
    [SerializeField]
    [Tooltip("The AudioClip that's played when the Dragable is dropped")]
    private AudioClip dropped;
    [SerializeField]
    [Tooltip("The AudioClip that's played when the Dragable is dropped")]
    private AudioClip stamped;

    [Header("Gizmos")]
    [SerializeField]
    private bool drawGizmos;

    private Vector2 offset;
    private Vector2 originalPos;

    private void Awake()
    {
        originalPos = transform.position;
    }

    private Document CheckForStampable()
    {
        IStampable[] stampables = FindObjectsOfType<Document>();
        foreach (Document _stampable in stampables)
        {
            if (Mathf.Abs(transform.position.x - _stampable.transform.position.x) < snapDistanceX / 2 &&
                    Mathf.Abs(transform.position.y - _stampable.transform.position.y) < snapDistanceY / 2)
            {
                return _stampable;
            }
            continue;
        }
        return null;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePos() - offset;
    }

    private void OnMouseUp()
    {
        FindObjectOfType<Manager>().IsGrabbing = false;

        if (CheckForStampable() != null && CheckForStampable().docStatus == DocumentStatus.Unstamped)
        {
            CheckForStampable().Stamp(stampType);
            audioSource.PlayOneShot(stamped);
            Invoke("ReturnToOriginalPos", 0.5f);
        }
        else
        {
            ReturnToOriginalPos();
        }
        audioSource.PlayOneShot(dropped);
    }

    private void ReturnToOriginalPos()
    {
        float timeElapsed = 0;
        while (timeElapsed < 4)
        {
            transform.position = Vector2.Lerp(transform.position, originalPos, timeElapsed / 4);
            timeElapsed += Time.deltaTime;
        }
        transform.position = originalPos;
    }

    private void OnMouseDown()
    {
        FindObjectOfType<Manager>().IsGrabbing = true;

        audioSource.PlayOneShot(pickup);
        offset = GetMousePos() - (Vector2)transform.position;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = new Color(1, 1, 1, 0.2f);
            Gizmos.DrawCube(transform.position, new Vector2(snapDistanceX, snapDistanceY));
        }
    }
}
