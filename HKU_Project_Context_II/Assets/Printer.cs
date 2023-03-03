using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cardDeck;

    [Header("Spawn Settings")]
    private int numberOfDecks;
    [SerializeField]
    private float timeBetweenSpawns;


    [Header ("Positioning")]
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    private Transform targetPos;
    [SerializeField]
    private float offsetToPrevious;
    
    private float timer = 0;

    public void Print()
    {
        if(cardDeck.Count == 0)
        {
            Debug.Log("CARD DECK IS EMPTY");
            return;
        }
        timer += Time.deltaTime;
        if (timer >= timeBetweenSpawns)
        {
            StartCoroutine(PrintDocument());
            timer = 0;
        }
    }

    private IEnumerator PrintDocument()
    {
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
        yield return null;
    }
}
