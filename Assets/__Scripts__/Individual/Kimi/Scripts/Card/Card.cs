using System;
using UnityEngine;
using UnityEngine.UI;


public enum CardOwner
{
    Player,
    Enemy
}
public class Card : MonoBehaviour
{
    [Header("Shared Data")]
    public CardData cardData;

    public CardCombat cardCombat;

    private bool isUICard = false;
    
    [Header("UI Only")]
    [SerializeField] private Image uiImage;
    
    [Header("2D Only")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public Animator animator;
    public RuntimeAnimatorController controller;
    
    public CardOwner cardOwner;
    
    private void Awake()
    {
        if (this.GetComponent<Card2D>() != null)
        {
            animator.enabled=false;
        }
        
        cardCombat = GetComponent<CardCombat>();
    }
    
    

    public void Init(CardData newData, bool asUICard)
    {
        cardData = newData;
        isUICard = asUICard;
        
        if (isUICard)
        {
            SetupUICard(cardData);
        }
        else
        {
            Setup2DCard(cardData);
        }
    }

    public void SetupUICard(CardData data)
    {
        if (uiImage == null)
        {
            return;
        }

        uiImage.sprite = data.Artwork;
        cardData.cardState = CardState.InHand;
    }

    public void Setup2DCard(CardData data)
    {
        if (_spriteRenderer == null)
        {
            return;
        }

        _spriteRenderer.sprite = cardData.Artwork;
        cardData.cardState = CardState.OnTable;
        
    }

    public CardData GetCardData() => cardData;

    public void PlayToBoard(Vector3 pos)
    {
        HandManager.Instance.PlayCardToWorld(this, pos, CardOwner.Player);
    }

    public void DeselectVisual()
    {
        var hover = GetComponent<CardHover>();
        if (hover != null)
        {
            hover.ForceDeselect();
        }
    }
    
    public void PlayDeathAnim()
    {
        animator.enabled = true;
        AnimatorOverrideController overrideController = new AnimatorOverrideController(controller);
        overrideController["Base_Death"] = cardData.deathClip; 

        animator.runtimeAnimatorController = overrideController;
        animator.Play("Base_Death");
    }
}