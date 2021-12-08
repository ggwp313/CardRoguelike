using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManagerScript : MonoBehaviour
{
    private static GameManagerScript instance;
    public static GameManagerScript Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Instance of " + nameof(GameManagerScript) + " is null");
            }
            return instance;
        }
    }

    public Game Game;
    public Transform enemyHand, playerHand;
    private void Start()
    {
        Game = new Game();

        GiveCardToHand(Game.enemyDeck, enemyHand);
        GiveCardToHand(Game.playerDeck, playerHand);
    }

    public void GiveHandCards(List<CardScriptableObject> p_deck, Transform p_hand)
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(p_deck, p_hand);
    }

    public void GiveCardToHand(List<CardScriptableObject> p_deck, Transform p_hand)
    {

    }
}
