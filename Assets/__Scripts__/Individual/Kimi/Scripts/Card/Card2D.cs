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
    private bool erased = false;
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
        if (!erased && CardCombat.currentHP>0)
        {
            HeartSprite.sprite = HeartSprites[CardCombat.currentHP-1];
        }
    }

    public void EraseSprites()
    {
        erased = true;
        HeartSprite.sprite = null;
    }
    public void DestroySelf()
    {
        Card card = GetComponent<Card>();
        if (card != null && card != null)
        {
            CardDeathNotifier.NotifyCardDeath(card);
            //Debug.Log($"Card has been destroyed.");
        }

        SlotReference refSlot = GetComponent<SlotReference>();
        if (refSlot != null && refSlot.slot != null)
        {
            if (card.cardData.ability != Ability.Livestock && card.cardData.ability != Ability.Grow)
            {
                refSlot.slot.ClearSlot();
            }
        }
        Destroy(gameObject);
    }

    public void ApplyVisual(CardData data)
    {
        Renderer.sprite = data.Artwork;
    }
}
