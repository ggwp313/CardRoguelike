using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIDisplay : MonoBehaviour
{
    [SerializeField] 
    private CardScriptableObject _cardInfo = null;

    public CardScriptableObject CardInfo
    {
        get
        {
            return _cardInfo;
        }
        set
        {
            _cardInfo = value;
        }
    }

    private bool _canAttack = false;

    [SerializeField]
    public TextMeshProUGUI _cardName = null;
    [SerializeField]
    private TextMeshProUGUI _cardDescription = null;
    [SerializeField]
    private TextMeshProUGUI _cardAttack = null;
    [SerializeField]
    private TextMeshProUGUI _cardHealth = null;
    [SerializeField]
    private TextMeshProUGUI _cardManacost = null;

    [SerializeField]
    private Image _cardArt = null;
    [SerializeField]
    private Image _cardBackArt = null;
    [SerializeField]
    private Image _cardHighlightedObj = null;


    void Start()
    {
        CardInitialize();
    }

    private void CardInitialize()
    {
        _cardName.text = _cardInfo.cardName;
        _cardDescription.text = _cardInfo.cardDescription;
        _cardAttack.text = _cardInfo.cardAttack.ToString();
        _cardHealth.text = _cardInfo.cardHealth.ToString();
        _cardManacost.text = _cardInfo.cardManacost.ToString();
        _cardArt.sprite = _cardInfo.cardArt;
        _canAttack = false;

    }

    public void HideCardInfo()
    {
        _cardBackArt.gameObject.SetActive(true);
    }
    public void ShowCardInfo()
    {
        _cardBackArt.gameObject.SetActive(false);
    }
    public void HighlightCard()
    {
        _cardHighlightedObj.gameObject.SetActive(true);
    }
    public void DeHighlightCard()
    {
        _cardHighlightedObj.gameObject.SetActive(false);
    }
    public void CahngeAttackState( bool p_can)
    {
        _canAttack = p_can;
    }
}
