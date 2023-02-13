using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dragable : MonoBehaviour
{
    [Header("Dragable Settings")]
    [Tooltip ("Determines the type of Dragable so the correct Target can accept it")]
    public Type type;

    [Tooltip("The time it takes for the Dragable to return to it's original position")]
    private float returnSpeed;

    [Header("Audio")]
    [SerializeField][Tooltip("The AudioSource that plays the Dragable SFX")]
    private AudioSource audioSource;

    [SerializeField][Tooltip("The AudioClip that's played when the Dragable is picked up")]
    private AudioClip pickup;

    [SerializeField][Tooltip("The AudioClip that's played when the Dragable is dropped")]
    private AudioClip dropped;

    [Header("Debug")]
    public bool placed;
    [SerializeField]
    private List<Target> possibleTargets;
    private Vector2 offset;
    private Vector2 originalPos;

    private void Awake()
    {
        originalPos = transform.position;
        FindPossibleTargets();
    }

    private void OnMouseDown()
    {
        audioSource.PlayOneShot(pickup);
        offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePos() - offset;  
    }

    private void OnMouseUp()
    {
        if(CheckForTarget()!= null)
        {
            CheckForTarget().Placed(this);
        }
        else
        {
            ReturnToOriginalPos();
        }
        audioSource.PlayOneShot(dropped);
    }

    private Target CheckForTarget()
    {
        foreach(Target target in possibleTargets)
        {
            if(Vector2.Distance(transform.position, target.transform.position) < target.snapDistance)
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
    private void ReturnToOriginalPos()
    {
        float timeElapsed = 0;
        while(timeElapsed < returnSpeed)
        {
            transform.position = Vector2.Lerp(transform.position, originalPos, timeElapsed/returnSpeed);
            timeElapsed += Time.deltaTime;
        }
        transform.position = originalPos;
    }
    private void FindPossibleTargets()
    {
        Target[] targets = FindObjectsOfType<Target>();
        foreach(Target target in targets)
        {
            if(target.compatibleDragables.HasFlag(type))
            {
                possibleTargets.Add(target);
            }
        }
    }

}
