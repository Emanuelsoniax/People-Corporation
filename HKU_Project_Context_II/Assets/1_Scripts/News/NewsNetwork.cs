using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewsNetwork : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI articleTitleText;
    [SerializeField]
    private TextMeshProUGUI articleIntroText;
    [SerializeField]
    private GameObject contentTemplate;
    [SerializeField]
    private GameObject contentContainer;

    [SerializeField]
    private NewsArticle[] articles;
    private NewsArticle articleToPublish;

    private void Start()
    {
        foreach (NewsArticle article in articles)
        {
            article.ResetContent();
        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        DisplayArticle(ChooseArticle());
    }

    public NewsArticle ChooseArticle()
    {
        List<int> contentFilesPerArticle = new List<int>();
        for(int i = 0; i < articles.Length; i++)
        {
            contentFilesPerArticle.Add(articles[i].UnlockedContent());
        }

        contentFilesPerArticle.Sort();
        contentFilesPerArticle.Reverse();

        for (int i = 0; i < articles.Length; i++)
        {
            if (articles[i].UnlockedContent() == contentFilesPerArticle[0]){
                articleToPublish = articles[i];
            }
        }

        return articleToPublish;
    }

    public void DisplayArticle(NewsArticle _articleToDisplay)
    {
        articleTitleText.text = _articleToDisplay.title;
        articleIntroText.text = _articleToDisplay.intro;

        //create content entries
        int contentDisplaying = 0;
        for (int i = 0; i < _articleToDisplay.content.Length; i++)
        {
            if (_articleToDisplay.content[i].displayInNews)
            {
                GameObject contentObject = Instantiate(contentTemplate, contentContainer.transform);
                contentObject.SetActive(true);
                
                //set position of content entry
                RectTransform rect = contentObject.GetComponent<RectTransform>();
                rect.anchoredPosition -= new Vector2(0, i * rect.sizeDelta.y);

                //set text of content entry
                contentObject.GetComponent<TextMeshProUGUI>().text = _articleToDisplay.content[i].contentText;
                contentDisplaying++;
            }
        }

        //Create outro object

        GameObject outroObject = Instantiate(contentTemplate, contentContainer.transform);
        outroObject.SetActive(true);

        //set position of content entry
        RectTransform outrorect = outroObject.GetComponent<RectTransform>();
        outrorect.anchoredPosition -= new Vector2(0, (contentDisplaying +1) * outrorect.sizeDelta.y);

        //set text of content entry
        outroObject.GetComponent<TextMeshProUGUI>().text = _articleToDisplay.outro;

    }

}
