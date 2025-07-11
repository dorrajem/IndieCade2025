using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    public List<Card> playerCards = new();
    public List<Card> enemyCards = new();

    private TurnManager turnManager;

    private bool attacking = false;
    private void Awake()
    {
        turnManager = GetComponent<TurnManager>();
        Instance = this;
    }

    void Update()
    {
        if (!attacking)
        {
            if (turnManager.gameTurn == GameTurn.PlayerCard)
            {
                PlayerCardAttack();
            }
            else if (turnManager.gameTurn == GameTurn.EnemyCard)
            {
                EnemyCardAttack();
            } 
        }
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
        StartCoroutine(AttackTime(playerCards));
    }

    private IEnumerator AttackTime(List<Card> Cards)
    {
        
        attacking = true;
        foreach (var card in Cards)
        {
            Debug.Log(card.cardData.CardName+":"+ card.cardOwner);
            if (card.GetCardData().cardState == CardState.OnTable)
            {
                if (card.cardCombat != null)
                {
                    card.cardCombat.TryAttack();
                    yield return new WaitForSeconds(1.5f);
                }
            }
        }

        if (turnManager.gameTurn == GameTurn.PlayerCard)
        {
            turnManager.gameTurn = GameTurn.EnemyCard;
        }
        else if (turnManager.gameTurn == GameTurn.EnemyCard)
        {
            turnManager.gameTurn = GameTurn.EnemyTurn;
            turnManager.EnemyTurn();
        }

        attacking = false;
    }

    public void EnemyCardAttack()
    {
        ClearDeadCards();
        StartCoroutine(AttackTime(enemyCards));
    }
}
