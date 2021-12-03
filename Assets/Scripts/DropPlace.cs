using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlace : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        CardBehaviour currentCard = eventData.pointerDrag.GetComponent<CardBehaviour>();

        if(currentCard)
        {
            currentCard.DefaultParent = transform;
        }

    }
}
