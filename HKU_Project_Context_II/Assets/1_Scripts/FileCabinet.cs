using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileCabinet : Target
{
    public override void Placed(Dragable document)
    {
        base.Placed(document);

        Document placedDoc = document.GetComponent<Document>();
        if (!placedDoc.placed)
        {
        if (placedDoc.docStatus == DocumentStatus.Unstamped)
        {
            Debug.Log("THIS HAS NO STAMP");
        }

        if (placedDoc.docStatus == DocumentStatus.Declined)
        {
            placedDoc.declinedValues.ApplyValues();
            placedDoc.placed = true;
            Destroy(placedDoc);
        }

        if (placedDoc.docStatus == DocumentStatus.Approved)
        {
            placedDoc.approvedValues.ApplyValues();
            placedDoc.newsContent.AcceptDoc();
            placedDoc.placed = true;
            Destroy(placedDoc);
        }

        }
    }
}
