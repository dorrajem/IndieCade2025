using System;
using UnityEngine;

public class CardCombat : MonoBehaviour
{
    public CardData cardData;

    [Header("Runtime")] 
    public int currentHP;
    public int attack;

    public float detectionRange = 5f;
    public LayerMask cardLayer;

    public TurnManager turnManager;

    private void Start()
    {
        currentHP = cardData.HealthPoint;
        attack = cardData.AttackPower;
    }

    public void TryAttack()
    {
        if (!turnManager.IsPlayerTurn || 
            cardData.cardState != CardState.OnTable || 
            cardData.cardState == CardState.Die)
        {
            return;
        }

        if (cardData.hasSpecialAbility)
        {
            cardData.OnSpecialAttack(this, turnManager);
        }
        PerformNormalAttack();
    }

    private void PerformNormalAttack()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, forward, out RaycastHit hit, detectionRange, cardLayer))
        {
            var enemy = hit.collider.GetComponent<CardCombat>();
            if (enemy != null)
            {
                enemy.ReceiveDamage(attack);
                return;
            }
        }
        
        // else take enemy's HP instead of card HP
    }

    public void ReceiveDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0 && cardData.cardState != CardState.Die)
        {
            Die();
        }
    }

    private void Die()
    {
        cardData.cardState = CardState.Die;
        Destroy(gameObject);
    }
}
