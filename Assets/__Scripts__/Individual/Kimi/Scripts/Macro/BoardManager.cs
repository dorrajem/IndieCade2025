using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    public List<Card> playerCards = new();
    public List<Card> enemyCards = new();


    private void Awake()
    {
        Instance = this;
    }

    public void RegisterCard(Card card)
    {
        if (card.cardData.cardOwner == CardOwner.Player)
        {
            playerCards.Add(card);
        }
        else
        {
            enemyCards.Add(card);
        }
    }

    public void ClearDeadCards()
    {
        playerCards.RemoveAll(c => c == null || c.cardData.cardState == CardState.Die);
        enemyCards.RemoveAll(c => c == null || c.cardData.cardState == CardState.Die);
    }

    public void PlayerCardAttack()
    {
        foreach (var card in playerCards)
        {
            if (card.cardData.cardState == CardState.OnTable && card.cardData.cardState != CardState.Die)
            {
                card.cardCombat.TryAttack();
            }
        }
    }
}
