using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
{
    

   private Vector3 _offset;
    private GameObject _tempCardGo;
    private Transform _defaultParent = null;
   private Transform _defaultTempCardParent;

    public Transform DefaultCardParent
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

    public Transform DefaultTempCardParent
    {
        get
        {
            return _defaultTempCardParent;
        }
        set
        {
            _defaultTempCardParent = value;
        }
    }

    private void Awake()
    {
        _tempCardGo = GameObject.Find("TempCard");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Card Offset
        _offset = transform.position - new Vector3(eventData.position.x, eventData.position.y, 0f);

        //Card & Temp Card Settings
        _defaultParent = transform.parent;

        _defaultTempCardParent = transform.parent;
        _tempCardGo.transform.SetParent(_defaultParent);
        _tempCardGo.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(_defaultParent.parent);

        //turn off CanvasGroup in order to IDropHandler work correctly
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Card Movement
        this.transform.position = new Vector3(eventData.position.x,eventData.position.y,0f) + _offset;

        if(_tempCardGo.transform.parent != _defaultTempCardParent)
        {
            _tempCardGo.transform.SetParent(_defaultTempCardParent);
        }

        CheckPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_defaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;// turn on back

        transform.SetSiblingIndex(_tempCardGo.transform.GetSiblingIndex());

        _tempCardGo.transform.SetParent(GameObject.Find("Canvas").transform);
        _tempCardGo.transform.localPosition = new Vector3(3000,0);
    }

    private void CheckPosition()
    {
        int newIndex = _defaultTempCardParent.childCount;

        for(int i = 0; i < _defaultTempCardParent.childCount; i ++)
        {
            if(transform.position.x < _defaultTempCardParent.GetChild(i).position.x)
            {
                newIndex = i;

                if(_tempCardGo.transform.GetSiblingIndex() < newIndex)
                {
                    newIndex--;
                }

                break;
            }
        }

        _tempCardGo.transform.SetSiblingIndex(newIndex);
    }
}
