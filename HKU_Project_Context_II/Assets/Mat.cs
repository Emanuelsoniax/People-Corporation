using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat : Target
{
    public override void Placed(Dragable document)
    {
        base.Placed(document);

        StartCoroutine(document.ScaleUp());
    }
}
