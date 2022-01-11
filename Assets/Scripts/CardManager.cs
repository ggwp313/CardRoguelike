using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _cardPrefab;
    public Transform playerHand;
    [SerializeField]
    private List<CardScriptableObject> _allScriptableObjects;

    public List<GameObject> test;

    private void Start()
    {
        for(int i = 0; i < _allScriptableObjects.Count; i ++)
        {
            //Dostaem ityj po spisku  scriptable odject i zapisywaem w  card.CardInfo
            CardUIDisplay card = new CardUIDisplay();
            card.CardInfo = _allScriptableObjects[i];
            //Teper inicyalizirujem nowuju kartu i podstawliem jej aktualnyi scriptable object 
            GameObject cardGo  = Instantiate(_cardPrefab, transform);
            cardGo.GetComponent<CardUIDisplay>().CardInfo = card.CardInfo;
            //Dobawliem w spisok vseh kart
            CardManagerScript._allCards.Add(cardGo);
        }

        test = CardManagerScript._allCards;
    }

    
}


public static class CardManagerScript
{
    public static List<GameObject> _allCards = new List<GameObject>();
}