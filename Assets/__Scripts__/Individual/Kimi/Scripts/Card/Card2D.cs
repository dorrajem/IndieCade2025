using System;
using UnityEngine;

public class Card2D : MonoBehaviour, ICardVisual
{
    public SpriteRenderer Renderer;


    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Mouse clicked");
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        SlotReference refSlot = GetComponent<SlotReference>();
        if (refSlot != null && refSlot.slot != null)
        {
            refSlot.slot.ClearSlot();
        }
        
        Destroy(gameObject);
    }

    public void ApplyVisual(CardData data)
    {
        Renderer.sprite = data.Artwork;
    }
}
