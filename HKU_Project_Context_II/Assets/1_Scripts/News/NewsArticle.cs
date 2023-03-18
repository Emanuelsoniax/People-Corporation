using UnityEngine;

[CreateAssetMenu(fileName = "NewsArticle")]
public class NewsArticle : ScriptableObject
{
    public string title;
    public string intro;

    [SerializeField]
    public NewsContent[] content;

    public string outro;

    public void ResetContent()
    {
        foreach(NewsContent _content in content)
        {
            _content.acceptedCards = 0;
            _content.displayInNews = false;
        }
    }

    public int UnlockedContent()
    {
        int unlocked = 0;
        for(int i = 0; i < content.Length; i++)
        {
            if (content[i].displayInNews)
            {
                unlocked++;
            }
        }

        Debug.Log(unlocked);

        return unlocked;
    }
}
