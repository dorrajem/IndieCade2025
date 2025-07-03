using System;
using UnityEngine;

public class Card2D : MonoBehaviour, ICardVisual
{
    public SpriteRenderer Renderer;
    
    private SacrificeManager SacrificeManager;
    void Start()
    {
        SacrificeManager = GameObject.FindWithTag("Manager").GetComponent<SacrificeManager>();
    }
    private void OnMouseDown()
    {
        if (SacrificeManager.Sacrificing && !SacrificeManager.CanPlace && !SacrificeManager.sacrifices.Contains(this.gameObject))
        {
            SacrificeManager.sacrifices.Add(this.gameObject);
        }
    }

    public void DestroySelf()
    {
        Card card = GetComponent<Card>();
        if (card != null && card.cardData != null)
        {
            CardDeathNotifier.NotifyCardDeath(card.cardData);
            Debug.Log($"Card has been destroyed.");
        }

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
