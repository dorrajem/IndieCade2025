using System;
using UnityEngine;

public class Card2D : MonoBehaviour, ICardVisual
{
    public SpriteRenderer Renderer;
    public SpriteRenderer HeartSprite;
    public Sprite[] HeartSprites;
    public Sprite[] SaplingSprites;
    public Sprite[] SacrificeSprites;
    
    private SacrificeManager SacrificeManager;
    private CardCombat CardCombat;
    void Start()
    {
        SacrificeManager = GameObject.FindWithTag("Manager").GetComponent<SacrificeManager>();
        CardCombat=GetComponent<CardCombat>();
    }
    private void OnMouseDown()
    {
        if (SacrificeManager.Sacrificing && !SacrificeManager.CanPlace && !SacrificeManager.sacrifices.Contains(this.gameObject))
        {
            SacrificeManager.sacrifices.Add(this.gameObject);
        }
    }

    void Update()
    {
        if (CardCombat.currentHP == 0)
        {
            HeartSprite.sprite = null;
        }

        for (int i = 0; i < CardCombat.currentHP; i++)
        {
            HeartSprite.sprite = HeartSprites[i];
        }
    }

    public void DestroySelf()
    {
        Card card = GetComponent<Card>();
        if (card != null && card.cardData != null)
        {
            CardDeathNotifier.NotifyCardDeath(card.cardData);
            //Debug.Log($"Card has been destroyed.");
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
