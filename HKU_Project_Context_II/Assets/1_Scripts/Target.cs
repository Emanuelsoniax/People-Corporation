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

    public virtual void Placed(Dragable document)
    {
        if (!document.placed)
        {
            Debug.Log("Dragable Placed");
            audioSource.PlayOneShot(placed);
        }

        if (snap)
        {
        float timeElapsed = 0;
        while (timeElapsed < 2)
        {
            document.transform.position = Vector2.Lerp(document.transform.position, transform.position, timeElapsed / 2);
            timeElapsed += Time.deltaTime;
        }
        document.transform.position = transform.position;
            document.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else
        {
            var offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - document.transform.position;
            document.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
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