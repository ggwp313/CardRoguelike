using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu( fileName = "Create New Card", menuName = "Cards/Minion Card" )]
public class CardScriptableObject : ScriptableObject
{
    [Header("Card Info")]
    public string cardName;
    public string cardDescription;
    public int cardManacost;

    [Header("Card Stats")]
    public int cardAttack;
    public int cardHealth;

    [Header("Card Art")]
    public Sprite cardArt;
}
