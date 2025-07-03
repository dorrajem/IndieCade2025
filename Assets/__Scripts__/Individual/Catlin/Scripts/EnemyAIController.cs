using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public EnemyAI enemyAI;
    public DeckData enemyDeckTemplate; 
    private DeckRuntime enemyCardDeck;

    private List<CardData> enemyHand = new();
    public int maxSaplingPoints = 5;
    private int currentSaplingPoints;
    
    public int currentDisasterPoints;
    public int maxDisasterPoints = 7;

    public int maxBoardSlots = 4;
    public int initialDrawCount = 5;

    private List<AIDropArea> enemyDropAreas = new();
    private List<DropArea> playerDropAreas = new(); 
    public bool canPlayDisaster;

    public int natureDeaths = 0;
    public int maxNatureDeaths = 10;

    private void Awake()
    {
        enemyDropAreas = new List<AIDropArea>(FindObjectsByType<AIDropArea>(FindObjectsSortMode.None));
    }

    private void Start()
    {
        currentSaplingPoints = maxSaplingPoints;
        currentDisasterPoints = 0;

        enemyCardDeck = new DeckRuntime();
        enemyCardDeck.LoadFromTemplate(enemyDeckTemplate);
        enemyCardDeck.Shuffle();

        DrawNewCard(initialDrawCount);
        canPlayDisaster = false; 
    }

    public void EnemyTakeTurn(TurnManager turnManager)
    {
        Debug.Log("Enemy Turn Starts");
        currentSaplingPoints += 2;
        currentSaplingPoints = Mathf.Min(currentSaplingPoints, maxSaplingPoints);
        SetDisasterPoints(2);

        DrawNewCard(1);

        int boardSpace = GetNumAvailableSlots();

        (currentSaplingPoints, currentDisasterPoints) = enemyAI.ExecuteTurn(
            enemyHand,
            (cardData) =>
            {
                AIDropArea slot = GetNextAvailableDropArea();
                if (slot != null)
                {
                    GameObject cardGO = new GameObject("EnemyCard");
                    Card card = cardGO.AddComponent<Card>();
                    card.Init(cardData, false);

                    slot.PlaceEnemyCard(card);
                    enemyHand.Remove(cardData);
                }
            },
            currentSaplingPoints,
            currentDisasterPoints,
            boardSpace
        );

        Debug.Log("Enemy Turn Ends");
        turnManager.TurnStart();
    }

    private void DrawNewCard(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CardData card = enemyCardDeck.Draw();
            if (card != null)
            {
                enemyHand.Add(card);
            }
        }
    }

    private AIDropArea GetNextAvailableDropArea()
    {
        return enemyDropAreas.Find(area => !area.isOccupied);
    }

    public int GetNumAvailableSlots()
    {
        int val = 0;
        foreach (var area in enemyDropAreas)
        {
            if (!area.isOccupied)
                val++;
        }
        return val;
    }

    public bool CheckCanPlayDisaster()
    {
        if (natureDeaths >= maxNatureDeaths)
        {
            canPlayDisaster = true;
            return true;
        }
        if (canPlayDisaster)
            return true;
       playerDropAreas = new List<DropArea>(FindObjectsByType<DropArea>(FindObjectsSortMode.None));
       foreach (var area in playerDropAreas)
       {
            if (area.isOccupied && area.placedCard != null)
            {
                Card card = area.placedCard.GetComponent<Card>();
                if (card != null && card.cardData.cardCategory == CardCategory.Industry)
                {
                    Debug.Log("Industry card found on player board");
                    canPlayDisaster = true;
                    return true;
                }
            }
       }
        return false; 
    }

    public void SetDisasterPoints(int increment)
    {
        if (!CheckCanPlayDisaster())
        {
            return;
        }
        currentDisasterPoints += increment;
        currentDisasterPoints = Mathf.Min(currentDisasterPoints, maxDisasterPoints);
    }
}