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

    public int maxBoardSlots = 4;
    public int initialDrawCount = 5;

    private List<AIDropArea> enemyDropAreas = new();

    private void Awake()
    {
        enemyDropAreas = new List<AIDropArea>(FindObjectsByType<AIDropArea>(FindObjectsSortMode.None));
    }

    private void Start()
    {
        currentSaplingPoints = maxSaplingPoints;

        enemyCardDeck = new DeckRuntime();
        enemyCardDeck.LoadFromTemplate(enemyDeckTemplate);
        enemyCardDeck.Shuffle();

        DrawInitialHand(initialDrawCount);
    }

    public void EnemyTakeTurn(TurnManager turnManager)
    {
        Debug.Log("Enemy Turn Starts");

        int boardSpace = GetNumAvailableSlots();

        currentSaplingPoints = enemyAI.ExecuteTurn(
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
            boardSpace
        );

        Debug.Log("Enemy Turn Ends");
        turnManager.TurnStart();
    }

    private void DrawInitialHand(int count)
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
}