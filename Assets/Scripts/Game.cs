using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public List<GameObject> playerDeck, enemyDeck,
                                      playerHand, enemyHand,
                                      playerField, enemyField;

    private void Start()
    {
        enemyDeck = GiveDeckCard();
        playerDeck = GiveDeckCard();

        enemyHand = new List<GameObject>();
        playerHand = new List<GameObject>();

        enemyField = new List<GameObject>();
        playerField = new List<GameObject>();
    }

    // initialization of player's adn enemie's deck
    List<GameObject> GiveDeckCard()
    {
        List<GameObject> list = new List<GameObject>();
        for(int i = 0; i < 10; i++)
        {
            list.Add(CardManagerScript._allCards[Random.Range(0, CardManagerScript._allCards.Count)]);
        }

        return list;
    }

    
}
