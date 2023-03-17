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

    // Start is called before the first frame update
    void Start()
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

        articleToPublish = articles[contentFilesPerArticle[0]];

        return articleToPublish;
    }

    public void DisplayArticle(NewsArticle _articleToDisplay)
    {
        articleTitleText.text = _articleToDisplay.title;
        articleIntroText.text = _articleToDisplay.intro;

        //create content entries

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
            }
        }

        //Create outro object

        GameObject outroObject = Instantiate(contentTemplate, contentContainer.transform);
        outroObject.SetActive(true);

        //set position of content entry
        RectTransform outrorect = outroObject.GetComponent<RectTransform>();
        outrorect.anchoredPosition -= new Vector2(0, _articleToDisplay.content.Length +1 * outrorect.sizeDelta.y);

        //set text of content entry
        outroObject.GetComponent<TextMeshProUGUI>().text = _articleToDisplay.outro;

    }

}
