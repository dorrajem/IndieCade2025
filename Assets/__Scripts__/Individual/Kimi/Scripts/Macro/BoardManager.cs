using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    
    public List<Card> playerCards = new();
    
    public List<Card> enemyCards = new();
    
    [Header("Score")]
    public Image ScoreSprite;
    public List<Sprite> scoreSprites = new();

    public int Score = 5;

    private TurnManager turnManager;

    private bool attacking = false;
    
    public GameObject WinPanel;
    public GameObject LosePanel;
    private void Awake()
    {
        turnManager = GetComponent<TurnManager>();
        Instance = this;
        Score = 5;
        Time.timeScale = 1;
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
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
        ScoreSprite.sprite = scoreSprites[Score];
        if (Score == 0)
        {
            LosePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Score == 10)
        {
            WinPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void TimeReturn()
    {
        Time.timeScale = 1;
    }
    public void RegisterCard(Card card, int index)
    {
        if (card.cardOwner == CardOwner.Player)
        {
            playerCards.Add(card);
            card.cardIndex = index;
            playerCards.Sort((a, b) => a.cardIndex.CompareTo(b.cardIndex));
        }
        else if (card.cardOwner == CardOwner.Enemy)
        {
            enemyCards.Add(card);
            card.cardIndex = index;
            enemyCards.Sort((a, b) => a.cardIndex.CompareTo(b.cardIndex));
        }
    }
    
    public void ClearDeadCards()
    {
        playerCards.RemoveAll(c => c == null || c.cardState == CardState.Die);
        enemyCards.RemoveAll(c => c == null || c.cardState == CardState.Die);
    }
    
    private IEnumerator AttackTime(List<Card> Cards)
    {
        
        attacking = true;
        foreach (var card in Cards)
        {
            if (card.cardState == CardState.OnTable)
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
    
    public void PlayerCardAttack()
    {
        ClearDeadCards();
        StartCoroutine(AttackTime(playerCards));
    }

    public void EnemyCardAttack()
    {
        ClearDeadCards();
        StartCoroutine(AttackTime(enemyCards));
    }
}
