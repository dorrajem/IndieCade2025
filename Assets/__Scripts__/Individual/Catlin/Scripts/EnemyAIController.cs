using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    [Header("Enemy AI")]
    public EnemyAI enemyAI;

    [Header("Enemy Deck")]
    public DeckRuntime enemyCardDeck;

    [Header("Enemy Hand")]
    private List<CardData> enemyHand = new();

    public int maxSaplingPoints = 5;
    private int currentSaplingPoints;

    [Header("Board Config")]
    public int maxBoardSlots = 4;

    public int initialDrawCount = 5;

    private void Start()
    {
        currentSaplingPoints = maxSaplingPoints;

        enemyCardDeck = new DeckRuntime();

        DrawInitialHand(initialDrawCount);
    }

    public void TakeTurn()
    {
        Debug.Log("Enemy Turn Starts");

        int boardSpace = maxBoardSlots - GetEnemyCardsOnBoard(); // Placeholder 

        currentSaplingPoints = enemyAI.ExecuteTurn(enemyHand, PlayCard, currentSaplingPoints, boardSpace);

        Debug.Log("Enemy Turn Ends");
    }

    private void DrawInitialHand(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CardData card = enemyCardDeck.Draw(); // Custom method you'll add
            if (card != null)
                enemyHand.Add(card);
        }
    }

    private void PlayCard(CardData card)
    {
        // Placeholder: will add card to board
        Debug.Log($"Enemy played: {card.CardName}");

    }

    private int GetEnemyCardsOnBoard()
    {
        // Placeholder 
        return 0;
    }
}