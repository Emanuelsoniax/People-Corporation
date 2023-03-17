using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewsContent")]
public class NewsContent : ScriptableObject
{
    public string contentText;
    public bool displayInNews;
    [Tooltip ("Number of cards accociaded with this content")]
    public int numberOfCards;
    public int acceptedCards;

    public void AcceptDoc()
    {
        acceptedCards++;

        if(acceptedCards >= numberOfCards)
        {
            displayInNews = true;
        }
    }
}
