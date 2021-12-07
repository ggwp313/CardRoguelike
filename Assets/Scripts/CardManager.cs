using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private List<CardScriptableObject> _allCards;

    private void Awake()
    {
        for(int i = 0; i < _allCards.Count; i ++)
        {
            CardManagerScript._allCards.Add(_allCards[i]);
        }

        for (int i = 0; i < CardManagerScript._allCards.Count; i++)
        {
            Debug.Log(CardManagerScript._allCards[i].cardName);
        }
            
    }

    
}


public static class CardManagerScript
{
    public static List<CardScriptableObject> _allCards = new List<CardScriptableObject>();
}