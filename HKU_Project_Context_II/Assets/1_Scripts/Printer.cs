using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> cardDeck;

    [Header("Spawn Settings")]
    private int numberOfDecks;
    [SerializeField]
    private float timeBetweenSpawns;


    [Header("Positioning")]
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    private Transform targetPos;
    [SerializeField]
    private float offsetToPrevious;

    private float timer = 0;

    [Header("Audio")]
    [SerializeField]
    private AudioClip printAudio;
    [SerializeField]
    private AudioSource source;
    public void Print()
    {
        if (cardDeck.Count == 0)
        {
            Debug.Log("CARD DECK IS EMPTY");
            return;
        }

        //Makes sure that not more than 4 documents can be in play at the same time
        if (FindObjectsOfType<Document>().Length < 4)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenSpawns)
            {
                StartCoroutine(PrintDocument());
                timer = 0;
            }
        }
    }

    private IEnumerator PrintDocument()
    {
        //speel audio af
        source.PlayOneShot(printAudio);

        //Get random card from deck
        GameObject cardToSpawn = cardDeck[Random.Range(0, cardDeck.Count)];
        //Spawn card at spawn position
        GameObject spawnedCard = Instantiate(cardToSpawn, spawnPos.position, spawnPos.rotation, null);
        //remove spawned card from deck
        cardDeck.Remove(cardToSpawn);
        //set target pos with offset
        Vector3 _targetpos = targetPos.position + new Vector3(Random.Range(0, offsetToPrevious), Random.Range(0, offsetToPrevious));
        //Animate card to target position
        float elapsedTime = 0;
        while (elapsedTime < 2)
        {
            spawnedCard.transform.position = Vector2.Lerp(spawnedCard.transform.position, _targetpos, (elapsedTime / 2));
            elapsedTime += Time.deltaTime;
            // Yield here
            yield return null;
        }
        // Make sure we got there
        spawnedCard.transform.position = _targetpos;
        spawnedCard.GetComponent<Dragable>().originalPos = _targetpos;
        yield return null;
    }
}
