using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType { 
    PlayerHand,
    PlayerField,
    EnemyHand,
    EnemyField 
}

public class DropPlace : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private FieldType _fieldType;

    public FieldType FieldType
    {
        get
        {
            return _fieldType;
        }
        set
        {
            _fieldType = value;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (_fieldType == FieldType.EnemyField || _fieldType == FieldType.EnemyHand || _fieldType == FieldType.PlayerHand)
            return;

        GameObject currentCard = eventData.pointerDrag;
        // Nelzia wystawit esli bolshe 6 na stole/esli ne hvataet many/esli ne nash hod
        if (currentCard && GameManagerScript.Instance.playerFieldCards.Count < 6 && GameManagerScript.Instance.IsPlayerTurn
            && GameManagerScript.Instance.PlayerMana >= currentCard.GetComponent<CardUIDisplay>().CardInfo.cardManacost)
        {

            GameManagerScript.Instance.playerFieldCards.Add(currentCard);

            GameManagerScript.Instance.playerHandCards.Remove(currentCard);

            currentCard.GetComponent<CardBehaviour>().DefaultCardParent = transform;

            GameManagerScript.Instance.ReduceMana(true,currentCard.GetComponent<CardUIDisplay>().CardInfo.cardManacost);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || _fieldType == FieldType.EnemyField || _fieldType == FieldType.EnemyHand || _fieldType == FieldType.PlayerHand)
            return;

        CardBehaviour currentCard = eventData.pointerDrag.GetComponent<CardBehaviour>();

        if (currentCard)
        {
            currentCard.DefaultTempCardParent = transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        CardBehaviour currentCard = eventData.pointerDrag.GetComponent<CardBehaviour>();

        if (currentCard && currentCard.DefaultTempCardParent == transform)
        {
            currentCard.DefaultTempCardParent = currentCard.DefaultCardParent;
        }
    }
}
