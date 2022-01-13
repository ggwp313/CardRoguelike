
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

    private int _enemyMana = 10;
    private int _playerMana = 10;
    private int _playerHealth = 30;
    private int _enemyHealth = 30;

    public int PlayerMana
    {
        get
        {
            return _playerMana;
        }
        set
        {
            _playerMana = value;
        }
    }

    public int EnemyMana
    {
        get
        {
            return _enemyMana;
        }
        set
        {
            _enemyMana = value;
        }
    }

    private int _turn, _turnTime = 30;

    [SerializeField]
    private TextMeshProUGUI _turnTimeText;
    [SerializeField]
    private TextMeshProUGUI _turnNumberText;
    [SerializeField]
    private Button _endTurnButton;

    [SerializeField]
    private TextMeshProUGUI _playerManapool;
    [SerializeField]
    private TextMeshProUGUI _enemyManapool;

    [SerializeField]
    private TextMeshProUGUI _playerHealthText;
    [SerializeField]
    private TextMeshProUGUI _enemyHealthText;

    [SerializeField]
    private GameObject _resultGO;
    [SerializeField]
    private TextMeshProUGUI _resultText;



    public List<GameObject> playerHandCards = new List<GameObject>();
    public List<GameObject> playerFieldCards = new List<GameObject>();
    public List<GameObject> enemyHandCards = new List<GameObject>();
    public List<GameObject> enemyFieldCards = new List<GameObject>();

    public bool IsPlayerTurn
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

        ShowMana();

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

        GameObject cardGo = Instantiate(card, p_hand, false);

        if (p_hand == enemyHand)
        {
            cardGo.GetComponent<CardUIDisplay>().HideCardInfo();
            enemyHandCards.Add(cardGo);
        }
        else
        {
            playerHandCards.Add(cardGo);
            //Chtoby igrok ne mog atakowac swoi karty
            cardGo.GetComponent<CardUIDisplay>().ShowCardInfo();
            cardGo.GetComponent<AttackedCard>().enabled = false;
        }
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();

        _turn++;

        _turnNumberText.text = "Turn - " + (_turn + 1).ToString();//test round

        _endTurnButton.interactable = IsPlayerTurn;

        if (IsPlayerTurn)
        {
            GiveNewCard();

            _playerMana = _enemyMana = 10;

            ShowMana();
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

        if (IsPlayerTurn)
        {
            //V nachale hoda delaet aktivnymi karty na stole u igroka
            foreach (var card in playerFieldCards)
            {
                card.GetComponent<CardUIDisplay>().CahngeAttackState(true);
                card.GetComponent<CardUIDisplay>().HighlightCard();
            }

            while (_turnTime-- > 0)
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

            while (_turnTime-- > 25)
            {
                _turnTimeText.text = _turnTime.ToString();
                yield return new WaitForSeconds(1);
            }
            //Enemy AI
            if (enemyHandCards.Count > 0)
            {
                EnemyTurn(enemyHandCards);
            }

        }
        ChangeTurn();
    }


    private void EnemyTurn(List<GameObject> p_cards)
    {
        int count = p_cards.Count == 1 ? 1 :
             Random.Range(0, p_cards.Count);
        //6 cards max on field
        for (int i = 0; i < count; i++)
        {
            if (enemyFieldCards.Count > 5 || _enemyMana == 0)
                return;

            List<GameObject> cardList = p_cards.FindAll(x => _enemyMana >= x.GetComponent<CardUIDisplay>().CardInfo.cardManacost);

            if (cardList.Count == 0)
            {
                break;
            }

            cardList[0].GetComponent<CardUIDisplay>().ShowCardInfo();
            cardList[0].transform.SetParent(enemyField);


            enemyFieldCards.Add(cardList[0]);

            enemyHandCards.Remove(cardList[0]);
        }

        foreach (var activeCard in enemyFieldCards.FindAll(x => x.GetComponent<CardUIDisplay>().CanAttack))
        {
            if (Random.Range(0, 2) == 0 && playerFieldCards.Count > 0)
            {
                var target = playerFieldCards[Random.Range(0, playerFieldCards.Count)];

                activeCard.GetComponent<CardUIDisplay>().CahngeAttackState(false);
                CardsFight(target, activeCard);
            }
            else
            {
                activeCard.GetComponent<CardUIDisplay>().CahngeAttackState(false);
                DamageHero(activeCard, false);
            }
        }

    }


    public void CardsFight(GameObject p_playerCard, GameObject p_enemyCard)
    {
        p_playerCard.GetComponent<CardUIDisplay>().GetDamage(p_enemyCard.GetComponent<CardUIDisplay>().CardInfo.cardAttack);
        p_enemyCard.GetComponent<CardUIDisplay>().GetDamage(p_playerCard.GetComponent<CardUIDisplay>().CardInfo.cardAttack);

        if (!p_playerCard.GetComponent<CardUIDisplay>().isAlive)
        {
            DestroyCards(p_playerCard);
        }
        else
        {
            p_playerCard.GetComponent<CardUIDisplay>().RefreshCardInfo();
        }

        if (!p_enemyCard.GetComponent<CardUIDisplay>().isAlive)
        {
            DestroyCards(p_enemyCard);
        }
        else
        {
            p_enemyCard.GetComponent<CardUIDisplay>().RefreshCardInfo();
        }
    }

    public void DestroyCards(GameObject p_card)
    {
        p_card.GetComponent<CardBehaviour>().OnEndDrag(null);

        if (enemyFieldCards.Exists(x => x == p_card))
            enemyFieldCards.Remove(p_card);

        if (playerFieldCards.Exists(x => x == p_card))
            playerFieldCards.Remove(p_card);

        Destroy(p_card);
    }

    public void ShowMana()
    {
        _playerManapool.text = _playerMana.ToString();
        _enemyManapool.text = _enemyMana.ToString();
    }

    public void ShowHP()
    {
        _enemyHealthText.text = _enemyHealth.ToString();
        _playerHealthText.text = _playerHealth.ToString();
    }

    public void ReduceMana(bool p_playerMana, int p_manacost)
    {
        if (p_playerMana)
        {
            _playerMana = Mathf.Clamp(_playerMana - p_manacost, 0, int.MaxValue);
        }
        else
        {
            _enemyMana = Mathf.Clamp(_enemyMana - p_manacost, 0, int.MaxValue);
        }
        ShowMana();
    }

    public void DamageHero(GameObject p_card, bool p_isEnemyAttacked)
    {
        if (p_isEnemyAttacked)
        {
            _enemyHealth = Mathf.Clamp(_enemyHealth - p_card.GetComponent<CardUIDisplay>().CardInfo.cardAttack, 0, int.MaxValue);
        }
        else
        {
            _playerHealth = Mathf.Clamp(_playerHealth - p_card.GetComponent<CardUIDisplay>().CardInfo.cardAttack, 0, int.MaxValue);
        }
        ShowHP();
        CheckForBattleResult();
    }

    public void CheckForBattleResult()
    {
        if (_enemyHealth == 0 || _playerHealth == 0)
        {
            _resultGO.SetActive(true);
            StopAllCoroutines();
        }

        if(_enemyHealth == 0)
        {
            _resultText.text = "VICTORY";
        }
        else
        {
            _resultText.text = "YOU LOST"; 
        }
    }
}
