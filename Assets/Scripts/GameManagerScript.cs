
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManagerScript : MonoBehaviour
{
    private static GameManagerScript _instance;

    public static GameManagerScript Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private Game Game;
    public GameObject cardPrefab;
    public Transform enemyHand, playerHand;
    public Transform enemyField, playerField;

    private int _turn, _turnTime = 30;

    [SerializeField]
    private TextMeshProUGUI _turnTimeText;
    [SerializeField]
    private TextMeshProUGUI _turnNumberText;
    [SerializeField]
    private Button _endTurnButton;


    public List<GameObject> playerHandCards = new List<GameObject>();
    public List<GameObject> playerFieldCards = new List<GameObject>();
    public List<GameObject> enemyHandCards = new List<GameObject>();
    public List<GameObject> enemyFieldCards = new List<GameObject>();

    public bool _isPlayerTurn
    {
        get
        {
            return _turn % 2 == 0;
        }
    }


    private void Start()
    {
        Game = GetComponent<Game>();

        _turn = 0;

        _turnNumberText.text = "Turn - " + (_turn + 1).ToString();//test round

        GiveStartHandCards(Game.enemyDeck, enemyHand);
        GiveStartHandCards(Game.playerDeck, playerHand);

        StartCoroutine(TurnFunc());
    } 

    public void GiveStartHandCards(List<GameObject> p_deck, Transform p_hand)
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(p_deck, p_hand);
    }

    public void GiveCardToHand(List<GameObject> p_deck, Transform p_hand)
    {
        if (p_deck.Count == 0)
            return;

        GameObject card = p_deck[0];

        p_deck.RemoveAt(0);

        GameObject cardGo = Instantiate(card,p_hand,false);

        if(p_hand == enemyHand)
        {
            cardGo.GetComponent<CardUIDisplay>().HideCardInfo();
            enemyHandCards.Add(cardGo);
        }
        else
        {
            playerHandCards.Add(cardGo);
        }
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();

        _turn++;

        _turnNumberText.text = "Turn - " + (_turn + 1).ToString();//test round

        _endTurnButton.interactable = _isPlayerTurn;
        
        if (_isPlayerTurn)
        {
            GiveNewCard();
        }

        StartCoroutine(TurnFunc());
    }
    // +1 card from deck on each player turn
    public void GiveNewCard()
    {
        GiveCardToHand(Game.enemyDeck, enemyHand);
        GiveCardToHand(Game.playerDeck, playerHand);
    }

    //Korutina Hoda
    IEnumerator TurnFunc()
    {
        _turnTime = 30;
        _turnTimeText.text = _turnTime.ToString();

        foreach (var card in playerFieldCards)
        {
            card.GetComponent<CardUIDisplay>().DeHighlightCard();
        }

        if(_isPlayerTurn)
        {   
            //V nachale hoda delaet aktivnymi karty na stole u igroka
            foreach (var card in playerFieldCards)
            {
                card.GetComponent<CardUIDisplay>().CahngeAttackState(true);
                card.GetComponent<CardUIDisplay>().HighlightCard();
            }

            while(_turnTime -- > 0)
            {
                _turnTimeText.text = _turnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            //V nachale hoda delaet aktivnymi karty na stole u enemy
            foreach (var card in enemyFieldCards)
                card.GetComponent<CardUIDisplay>().CahngeAttackState(true);

            while (_turnTime -- > 25)
            {
                _turnTimeText.text = _turnTime.ToString();
                yield return new WaitForSeconds(1);
            }
            //Enemy AI
            if(enemyHandCards.Count > 0)
            {
                EnemyTurn(enemyHandCards);
            }

        }
        ChangeTurn();
    }


    private void EnemyTurn(List<GameObject> p_cards)
    {
        int count = p_cards.Count == 1 ? 1 :
             Random.Range(0,p_cards.Count);
        //6 cards max on field
        for(int i = 0; i < count; i ++)
        {
            if (enemyFieldCards.Count > 5)
                return;

            p_cards[0].GetComponent<CardUIDisplay>().ShowCardInfo();
            p_cards[0].transform.SetParent(enemyField);


            enemyFieldCards.Add(p_cards[0]);
            enemyHandCards.Remove(p_cards[0]);
        }
    }

}
