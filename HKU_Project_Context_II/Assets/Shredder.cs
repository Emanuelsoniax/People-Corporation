using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Document;

public class Shredder : Target
{
    [SerializeField]
    private DocValues values;

    [SerializeField]
    private Transform endOfShredder;

    public override void Placed(Dragable document)
    {
        base.Placed(document);

        Shred(document.gameObject);

        values.ApplyValues();
    }

    private void Shred(GameObject documentToShred)
    {
        StartCoroutine(MoveThroughShredder(documentToShred.transform));
        Destroy(documentToShred,6);
    }

    private IEnumerator MoveThroughShredder(Transform documentToShred)
    {
        float elapsedTime = 0;
        while (elapsedTime < 10)
        {
            documentToShred.position = Vector2.Lerp(documentToShred.position, endOfShredder.position, (elapsedTime / 10));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Make sure we got there
        documentToShred.position = endOfShredder.position;
        yield return null;
    }
}
