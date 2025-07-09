using System;
using System.Collections.Generic;
using UnityEngine;

public class CardCombat : MonoBehaviour, IDamageable
{
    public Card _card;

    [Header("Runtime")] 
    public int currentHP { get; private set; }
    public int attack { get; private set; }

    private IAttackBehavior _attackBehavior;
    private AttackHolder _basicAttack;

    public float detectionRange = 5f;
    public LayerMask cardLayer;
    
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Manager").GetComponent<AudioManager>();
        _card = GetComponent<Card>();
        var holder = GetComponent<AttackHolder>();
        _attackBehavior = holder != null ? holder.GetBehavior() : null;
        _basicAttack = GetComponent<AttackHolder>();
    }

    private void Start()
    {
        var data = _card.GetCardData();
        currentHP = data.HealthPoint;
        attack = data.AttackPower;
    }

    public void TryAttack()
    {
        if (!TurnManager.Instance.IsPlayerTurn || _card.GetCardData().cardState != CardState.OnTable || _card.GetCardData().cardState == CardState.Die)
        {
            return;
        }

        if (_card.GetCardData().hasSpecialAbility && _card.GetCardData().OnSpecialAttack != null)
        {
            Debug.LogWarning("Special attack applied");
            // apply special attack
        }
        else
        {
            audioManager.PlayCardAttack();
            _attackBehavior?.ExecuteAttack(this);
        }
    }

    public void TakeDamage(int amount)
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
        audioManager.PlayCardDie();
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_attackBehavior is BasicAttack basicAttack)
        {
            Vector3 start = transform.position;

            Vector3 dir = Application.isPlaying ? GetAttackDirection() : transform.up;
            Vector3 end = transform.position + dir * 3f;

            List<Vector3> points = basicAttack.GetParabolaPoints(start, end, basicAttack.arcHeight, 20);

            Gizmos.color = Color.blue;
            for (int i = 0; i < points.Count - 1; i++)
            {
                Gizmos.DrawLine(points[i], points[i + 1]);
                Gizmos.DrawSphere(points[i], 0.05f);
                Debug.Log($"Sample Point : {i} : {points[i]}");

                Collider[] hits = Physics.OverlapSphere(points[i], 0.3f, cardLayer);
                Debug.Log($"Hits : {hits.Length} objects");
                foreach (var hit in hits)
                {
                    Debug.Log($"{hit.name}, layer : {hit.gameObject.layer}");
                }
            }
        }
    }

#endif

    public Vector3 GetAttackDirection()
    {
        return _card.GetCardData().cardOwner == CardOwner.Player 
            ? transform.up 
            : -transform.up;
    }
}
