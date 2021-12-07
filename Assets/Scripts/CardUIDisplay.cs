using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIDisplay : MonoBehaviour
{
    [SerializeField] 
    private CardScriptableObject _cardInfo = null;

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
    }
}
