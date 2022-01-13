using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackedHero : MonoBehaviour, IDropHandler
{
    public enum HeroType
    { 
        Player,
        Enemy
    }

    public HeroType type;

    public void OnDrop(PointerEventData eventData)
    {
        if (!GameManagerScript.Instance.IsPlayerTurn)
            return;

        GameObject card = eventData.pointerDrag;

        if(card && card.GetComponent<CardUIDisplay>().CanAttack && type == HeroType.Enemy)
        {
            card.GetComponent<CardUIDisplay>().CanAttack = false;
            GameManagerScript.Instance.DamageHero(card,true);
        }
           
    }
}
 