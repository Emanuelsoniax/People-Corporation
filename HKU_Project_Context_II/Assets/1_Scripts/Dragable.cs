using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    [Header("Dragable Settings")]
    [Tooltip("Determines the type of Dragable so the correct Target can accept it")]
    public Type type;

    [Tooltip("The time it takes for the Dragable to return to it's original position")]
    private float returnSpeed;

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

    [Header("Debug")]
    [HideInInspector]
    public bool placed;
    private List<Target> possibleTargets = new List<Target>();
    private Vector2 offset;
    [HideInInspector]
    public Vector2 originalPos;
    [SerializeField]
    private Vector3 upscaling;

    private void Awake()
    {
        originalPos = transform.position;

        FindPossibleTargets();

    }

    private void OnMouseDown()
    {
        if (!placed)
        {
            FindObjectOfType<Manager>().IsGrabbing = true;
            audioSource.PlayOneShot(pickup);
            offset = GetMousePos() - (Vector2)transform.position;
            transform.eulerAngles = new Vector3(0, 0, 0);
            //StartCoroutine(ScaleDown());
        }
    }

    private void OnMouseDrag()
    {
        if (!placed)
        {
            transform.position = GetMousePos() - offset;
        }
    }

    private void OnMouseUp()
    {
        FindObjectOfType<Manager>().IsGrabbing = false;

        if (CheckForTarget() != null)
        {
            CheckForTarget().Placed((Document)this);
        }
        else
        {
            StartCoroutine(ReturnToOriginalPos());
        }
        audioSource.PlayOneShot(dropped);
    }

    private IEnumerator ScaleDown()
    {
        float timeElapsed = 0;
        while (timeElapsed < 2)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector3(1,1,1), timeElapsed / 2);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 1);
        yield return null;
    }

    public IEnumerator ScaleUp()
    {
        float timeElapsed = 0;
        while (timeElapsed < 2)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, upscaling, timeElapsed / 2);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = upscaling;
        yield return null;
    }

    private Target CheckForTarget()
    {
        foreach (Target target in possibleTargets)
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) < target.snapDistanceX / 2 &&
                Mathf.Abs(transform.position.y - target.transform.position.y) < target.snapDistanceY / 2)
            {
                return target;
            }
            continue;
        }
        return null;
    }

    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private IEnumerator ReturnToOriginalPos()
    {
        float timeElapsed = 0;
        while (timeElapsed < returnSpeed)
        {
            transform.position = Vector2.Lerp(transform.position, originalPos, timeElapsed / returnSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
        yield return null;
    }
    private void FindPossibleTargets()
    {
        Target[] targets = FindObjectsOfType<Target>();
        foreach (Target target in targets)
        {
            if (target.compatibleDragables.HasFlag(type))
            {
                possibleTargets.Add(target);
            }
            else
            {
                continue;
            }
        }
    }

}
