using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Flags]
public enum Type { Doc1 = 1, Doc2 = 2, Doc3 = 4};

public class Target : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("Enables snapping the dragable to the Target")]
    [SerializeField]
    private bool snap;
    [Tooltip("The distance in which a Dragable will be able to snap to the Target")]
    public float snapDistanceX;
    [Tooltip("The distance in which a Dragable will be able to snap to the Target")]
    public float snapDistanceY;
    [Tooltip("Determines the type of Dragable the Target will accept")]
    public Type compatibleDragables;

    [Header("Audio")]
    [SerializeField][Tooltip("The AudioSource that plays the Target SFX")]
    private AudioSource audioSource;
    [SerializeField][Tooltip("The AudioClip that's played when the Dragable is placed on the Target")]
    private AudioClip placed;

    [Header("Gizmos")]
    [SerializeField]
    private bool drawGizmos;

    [Tooltip ("Functions placed here will be excecuted once something is placed on this target")]
    public OnPlacedEvent OnPlaced;

    public void Placed(Dragable dragable)
    {
        Debug.Log("Dragable Placed");
        audioSource.PlayOneShot(placed);

        if (snap)
        {
        float timeElapsed = 0;
        while (timeElapsed < 2)
        {
            dragable.transform.position = Vector2.Lerp(dragable.transform.position, transform.position, timeElapsed / 2);
            timeElapsed += Time.deltaTime;
        }
        dragable.transform.position = transform.position;
            dragable.transform.rotation = transform.rotation;
        }
        else
        {
            var offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - dragable.transform.position;
            dragable.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
        }

        OnPlaced.Invoke();
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

[System.Serializable]
public class OnPlacedEvent : UnityEvent { }