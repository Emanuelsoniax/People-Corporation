using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(BoxCollider))]
public class Interactable : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip audioClip;


    private void OnMouseDown()
    {
        if(animator != null)
        {
        animator.SetTrigger("animate");
        }
        if(source != null)
        {
        source.PlayOneShot(audioClip);
        }
    }
}
