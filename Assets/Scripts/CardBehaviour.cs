using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
{
    

   private Vector3 _offset;
   private Transform _defaultParent;

    private GameObject _tempCardGo;
    private Transform _defaultTempCardParent;



    public Transform DefaultParent
    {
        get
        {
            return _defaultParent;
        }
        set
        {
            _defaultParent = value;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _offset = transform.position - new Vector3(eventData.position.x, eventData.position.y, 0f);

        _defaultParent = transform.parent;

        transform.SetParent(_defaultParent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = new Vector3(eventData.position.x,eventData.position.y,0f) + _offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_defaultParent);

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
