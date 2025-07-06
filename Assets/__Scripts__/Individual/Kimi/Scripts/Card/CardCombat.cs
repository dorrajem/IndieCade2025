using System;
using UnityEngine;

public class CardCombat : MonoBehaviour
{
    private Card _card;

    [Header("Runtime")] 
    public int currentHP;
    public int attack;

    public float detectionRange = 5f;
    public LayerMask cardLayer;

    public TurnManager turnManager;


    private void Awake()
    {
        _card = GetComponent<Card>();
    }

    private void Start()
    {
        currentHP = _card.GetCardData().HealthPoint;
        attack = _card.GetCardData().AttackPower;
    }

    public void TryAttack()
    {
        if (!turnManager.IsPlayerTurn || 
            _card.GetCardData().cardState != CardState.OnTable || 
            _card.GetCardData().cardState == CardState.Die)
        {
            return;
        }

        if (_card.GetCardData().hasSpecialAbility)
        {
            _card.GetCardData().OnSpecialAttack(this, turnManager);
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
        if (currentHP <= 0 && _card.GetCardData().cardState != CardState.Die)
        {
            Die();
        }
    }

    private void Die()
    {
        _card.GetCardData().cardState = CardState.Die;
        Destroy(gameObject);
    }
}
