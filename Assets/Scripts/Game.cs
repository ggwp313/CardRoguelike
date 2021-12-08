using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public List<CardScriptableObject> playerDeck, enemyDeck,
                                      playerHand, enemyHand,
                                      playerField, enemyField;

    public Game()
    {
        enemyDeck = GiveDeckCard();
        playerDeck = GiveDeckCard();

        enemyHand = new List<CardScriptableObject>();
        playerHand = new List<CardScriptableObject>();

        enemyField = new List<CardScriptableObject>();
        playerField = new List<CardScriptableObject>();
    }

    List<CardScriptableObject> GiveDeckCard()
    {
        List<CardScriptableObject> list = new List<CardScriptableObject>();
        for(int i = 0; i < 10; i++)
        {
            list.Add(CardManagerScript._allCards[Random.Range(0, CardManagerScript._allCards.Count)]);
        }

        return list;
    }

    
}
