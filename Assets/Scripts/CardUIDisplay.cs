using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIDisplay : MonoBehaviour
{
    [SerializeField] 
    private CardScriptableObject _cardInfo;

    [SerializeField]
  public TextMeshProUGUI _cardName;
    [SerializeField]
    private TextMeshProUGUI _cardDescription;
    [SerializeField]
    private TextMeshProUGUI _cardAttack;
    [SerializeField]
    private TextMeshProUGUI _cardHealth;
    [SerializeField]
    private TextMeshProUGUI _cardManacost;

    [SerializeField]
    private Image _cardArt;
    [SerializeField]
    private Image _cardBackgroundImage;

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
