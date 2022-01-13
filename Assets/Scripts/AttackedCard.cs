using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackedCard : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (!GameManagerScript.Instance.IsPlayerTurn)
            return;
        

        GameObject card = eventData.pointerDrag;

        if(card && card.GetComponent<CardUIDisplay>().CanAttack && transform.parent == GameManagerScript.Instance.enemyField)
        {
            card.GetComponent<CardUIDisplay>().CahngeAttackState(false);
            GameManagerScript.Instance.CardsFight(card , this.gameObject);
        }
    }
}
