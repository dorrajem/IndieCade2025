using System;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public Transform handArea;
    public GameObject cardPrefab;

    public static HandManager Instance;
    private List<GameObject> handCards = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddCardToHand(CardData cardData)
    {
        GameObject cardGO = Instantiate(cardPrefab, handArea);
        cardGO.GetComponent<CardDisplay>().Setup(cardData);
        handCards.Add(cardGO);
        
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
        float spacing = 100f;
        float startX = -(handCards.Count - 1) * spacing / 2f;

        for (int i = 0; i < handCards.Count; i++)
        {
            var rt = handCards[i].GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(startX + i * spacing, 0);
            handCards[i].transform.SetSiblingIndex(i);
        }
    }
}
