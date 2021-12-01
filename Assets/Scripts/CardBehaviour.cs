using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    

    void Awake()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Vector3 newPos = Camera.main.ScreenToWorldPoint(eventData.position);
        newPos.z = 0;
        transform.position = newPos;

        //Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x,eventData.position.y,0));
        //transform.position = newPos;

        //Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = newPos;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
