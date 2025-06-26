using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("Shared Data")]
    public CardData cardData;

    private bool isUICard = false;
    
    [Header("UI Only")]
    [SerializeField] private Image uiImage;
    
    [Header("2D Only")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    

    public void Init(CardData newData)
    {
        cardData = newData;

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
    }

    public void Setup2DCard(CardData data)
    {
        if (_spriteRenderer == null)
        {
            return;
        }

        _spriteRenderer.sprite = cardData.Artwork;
    }
    


    public CardData GetCardData() => cardData;

    public void PlayToBoard(Vector3 pos)
    {
        HandManager.Instance.PlayCardToWorld(this, pos);
    }

}