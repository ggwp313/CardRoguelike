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

    private int _turn, _turnTime = 30;

    [SerializeField]
    private TextMeshProUGUI _turnTimeText;
    [SerializeField]
    private Button _endTurnButton;

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

        Instantiate(card,p_hand,false);
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();

        _turn++;

        _endTurnButton.interactable = _isPlayerTurn;
        
        if (_isPlayerTurn)
        {
            GiveNewCard();
        }

        StartCoroutine(TurnFunc());
    }

    public void GiveNewCard()
    {
        GiveCardToHand(Game.enemyDeck, enemyHand);
        GiveCardToHand(Game.playerDeck, playerHand);
    }

    IEnumerator TurnFunc()
    {
        _turnTime = 30;
        _turnTimeText.text = _turnTime.ToString();

        if(_isPlayerTurn)
        {
            while(_turnTime -- > 0)
            {
                _turnTimeText.text = _turnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {

            while(_turnTime -- > 20)
            {
                _turnTimeText.text = _turnTime.ToString();
                yield return new WaitForSeconds(1);
            }

        }
        ChangeTurn();
    }

}
