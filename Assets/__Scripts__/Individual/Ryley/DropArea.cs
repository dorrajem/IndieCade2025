using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//please use this to check if zones are correct for later placement

public class DropArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("OnPointerEnter");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerexit");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        //CardDragHandler d = eventData.pointerDrag.GetComponent<CardDragHandler>();
        //   if (d != null)
        //    {
        //        d.originalParent = transform;
        //    }
    }


}
