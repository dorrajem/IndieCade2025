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
        if (card.cardOwner == CardOwner.Player)
        {
            playerCards.Add(card);
        }
        else if (card.cardOwner == CardOwner.Enemy)
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
        ClearDeadCards();
        foreach (var card in playerCards)
        {
            if (card.GetCardData().cardState == CardState.OnTable)
            {
                if (card.cardCombat != null)
                {
                    card.cardCombat.TryAttack();
                }
                else
                {
                    //Debug.LogWarning($"{card.name} has no CardCombat assigned!");
                }
            }
        }
    }

    public void EnemyCardAttack()
    {
        ClearDeadCards();
        foreach (var card in enemyCards)
        {
            if (card.GetCardData().cardState == CardState.OnTable)
            {
                card.cardCombat.TryAttack();
            }
        }
    }
}
