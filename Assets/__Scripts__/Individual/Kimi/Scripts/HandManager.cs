using System;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [Header("Layout Config")]
    public Transform origin;
    public RectTransform handArea;
    public GameObject cardPrefab;

    [Header("Spacing Setting")] 
    public float defaultSpacing = 160f;
    public float cardMinSpacing = 60f;

    public static HandManager Instance;
    private List<GameObject> handCards = new ();

    private void Awake()
    {
        Instance = this;
    }

    public void AddCardToHand(CardData cardData)
    {
        GameObject cardGO = Instantiate(cardPrefab, handArea);
        cardGO.GetComponent<CardDisplay>().Setup(cardData);
        handCards.Add(cardGO);

        cardData.cardState = CardState.OnTable;
        
        UpdateCardPositions();
    }

    public void RemoveCardFromHand(GameObject cardGO)
    {
        handCards.Remove(cardGO);
        Destroy(cardGO);
        UpdateCardPositions();
    }

    public void UpdateCardPositions()
    {
        int totalCards = handCards.Count;
        if (totalCards == 0) return;

        float areaWidth = handArea.rect.width;
        
        // Handle max width
        float totalDefaultWidth = (totalCards - 1) * defaultSpacing;
        float spacing = defaultSpacing;
        if (totalDefaultWidth > areaWidth)
        {
            spacing = Mathf.Max(cardMinSpacing, areaWidth / (totalCards - 1));
        }

        float totalWidth = spacing * (totalCards - 1);
        float startX = -totalWidth / 2f;
        Vector3 originPos = origin.localPosition;

        for (int i = 0; i < totalCards; i++)
        {
            RectTransform cardRT = handCards[i].GetComponent<RectTransform>();
            float xOffset = startX + i * spacing;
            cardRT.anchoredPosition = new Vector2(originPos.x + xOffset, 0);
            handCards[i].transform.SetSiblingIndex(i);
        }
    }
}
