using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICardDisplay : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Button selectionButton;

    private CardData _cardData;

    public void SetUp(CardData cardData, Action<CardData> onSelected)
    {
        _cardData = cardData;
        cardImage.sprite = cardData.Artwork;
        
        selectionButton.onClick.RemoveAllListeners();
        selectionButton.onClick.AddListener(() => onSelected?.Invoke(cardData));
    }
}
