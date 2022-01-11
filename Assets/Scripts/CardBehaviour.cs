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

   private bool _isDraggable;


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

        // save the current card parent transform(Player Hand)
        _defaultParent = transform.parent;

        //We can only move cards from our hand and at our turn
        _isDraggable = (_defaultParent.GetComponent<DropPlace>().FieldType == FieldType.PlayerHand) && GameManagerScript.Instance._isPlayerTurn;

        if (!_isDraggable)
            return;
        
        _defaultTempCardParent = transform.parent;
        _tempCardGo.transform.SetParent(_defaultParent);
        _tempCardGo.transform.SetSiblingIndex(transform.GetSiblingIndex());

        //Change current card parent from Player Hand to Board
        transform.SetParent(_defaultParent.parent);

        //  IDropHandler work correctly
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDraggable)
            return;

        //Card Movement
        this.transform.position = new Vector3(eventData.position.x,eventData.position.y,0f) + _offset;
        //Temp card movement
        if(_tempCardGo.transform.parent != _defaultTempCardParent)
        {
            _tempCardGo.transform.SetParent(_defaultTempCardParent);
        }

        CheckPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_isDraggable)
            return;

        transform.SetParent(_defaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;// turn on back taycast

        //Chtoby karta pojawlialas ne skraju a na meste temp karty
        transform.SetSiblingIndex(_tempCardGo.transform.GetSiblingIndex());

        //Uberaem tmep kartu podalshe ot ekrana kamery
        _tempCardGo.transform.SetParent(GameObject.Find("Canvas").transform);// TODO: FIND
        _tempCardGo.transform.localPosition = new Vector3(3000, 0);// TODO: string 
    }

    //Moves temp card position depending on current card position
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
