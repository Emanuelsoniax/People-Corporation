using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StampType { Decline, Approve}
public class Stamp : Dragable
{
    [SerializeField]
    private StampType stampType;

    
}
