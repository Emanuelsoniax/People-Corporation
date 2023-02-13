using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Flags]
public enum Type { Doc1 = 1, Doc2 = 2, Doc3 = 4};

public class Target : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("The distance in which a Dragable will be able to snap to the Target")]
    public float snapDistance;
    [Tooltip("Determines the type of Dragable the Target will accept")]
    public Type compatibleDragables;

    [Header("Audio")]
    [SerializeField][Tooltip("The AudioSource that plays the Target SFX")]
    private AudioSource audioSource;
    [SerializeField][Tooltip("The AudioClip that's played when the Dragable is placed on the Target")]
    private AudioClip placed;

    public void Placed(Dragable dragable)
    {
        Debug.Log("Dragable Placed");
        audioSource.PlayOneShot(placed);

        float timeElapsed = 0;
        while (timeElapsed < 2)
        {
            dragable.transform.position = Vector2.Lerp(dragable.transform.position, transform.position, timeElapsed / 2);
            timeElapsed += Time.deltaTime;
        }
        dragable.transform.position = transform.position;

    }
}
