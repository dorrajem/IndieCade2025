using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    public List<CardCombat> playerCards = new();
    public List<CardCombat> enemyCards = new();


    public void RegisterCard(CardCombat card)
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
                card.TryAttack();
            }
        }
    }
}
