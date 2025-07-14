using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardCombat : MonoBehaviour, IDamageable
{
    public Card _card;

    [Header("Runtime")] 
    public int currentHP { get; private set; }
    public int attack { get; private set; }

    public float detectionRange = 5f;
    public LayerMask cardLayer;
    
    private AudioManager audioManager;
    private TurnManager turnManager;
    private BoardManager boardManager;
    private Card2D card2d;
    
    [Header("Attack Settings")]
    public float range = 5f;
    public float width = 1f; 
    public float height = 1f;  
    public LayerMask targetLayer;

    private void Awake()
    {
        turnManager = GameObject.FindWithTag("Manager").GetComponent<TurnManager>();
        audioManager = GameObject.FindWithTag("Manager").GetComponent<AudioManager>();
        boardManager = GameObject.FindWithTag("Manager").GetComponent<BoardManager>();
        _card = GetComponent<Card>();
        card2d = GetComponent<Card2D>();
    }
    
    private void Start()
    {
        var data = _card.GetCardData();
        currentHP = data.HealthPoint;
        attack = data.AttackPower;
    }

    public void TryAttack()
    {
        if ((turnManager.gameTurn!=GameTurn.PlayerCard && turnManager.gameTurn!=GameTurn.EnemyCard) || _card.cardState != CardState.OnTable || _card.cardState == CardState.Die || _card.GetCardData().AttackPower==0)
        {
            return;
        }
        
        audioManager.PlayCardAttack();
        ExecuteAttack(this);
        StartCoroutine(AttackMove());
        
    }
    
    public void ExecuteAttack(CardCombat attacker)
    {
        Vector3 origin = attacker.transform.position;
        Vector3 direction = attacker.GetAttackDirection();
        
        Vector3 halfExtents = new Vector3(width * 0.8f, height *1.6f, range*0.25f);
        Vector3 center = origin + direction * (range*0.66f);
        
        // Detect cards in the attack box
        Collider[] hits = Physics.OverlapBox(
            center, 
            halfExtents, 
            Quaternion.LookRotation(direction), 
            targetLayer
        );
        // Damage all targets in th box
        if (hits.Length != 0)
        {
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out CardCombat targetCard))
                {
                    targetCard.TakeDamage(attack);
                }
            }
        }
        else
        {
            if (attacker._card.cardOwner == CardOwner.Player)
            {
                boardManager.Score = Mathf.Min(10,boardManager.Score+attack);
            }
            else if (attacker._card.cardOwner == CardOwner.Enemy)
            {
                boardManager.Score = Mathf.Max(0,boardManager.Score-attack);
            }
        }
       
    }

    private IEnumerator AttackMove()
    {
        Vector3 dir = GetAttackDirection()+new Vector3(0,0,-0.1f);
        Vector3 home=transform.position;
        transform.position = Vector3.Lerp(transform.position, transform.position+(dir*3), 1f);
        yield return new WaitForSeconds(0.5f);
        
        transform.position = Vector3.Lerp(transform.position, home, 1f);
        yield return new WaitForSeconds(0.5f);
        transform.position=home;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0 && _card.cardState != CardState.Die)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        _card.cardState = CardState.Die;
        audioManager.PlayCardDie();
        _card.PlayDeathAnim();

        yield return new WaitForSeconds(1f); 

        card2d.DestroySelf();
    }

#if UNITY_EDITOR
private void OnDrawGizmosSelected()
{
    
    Bounds box = GetAttackBoxBounds(this);
    Gizmos.color = Color.red;
    Gizmos.matrix = Matrix4x4.TRS(box.center, Quaternion.LookRotation(GetAttackDirection()), Vector3.one);
    Gizmos.DrawWireCube(Vector3.zero, box.size);
    
}
#endif
    
    public Vector3 GetAttackDirection()
    {
        return _card.cardOwner == CardOwner.Player 
            ? transform.up 
            : -transform.up;
    }
    
    public Bounds GetAttackBoxBounds(CardCombat attacker)
    {
        Vector3 origin = attacker.transform.position;
        Vector3 direction = attacker.GetAttackDirection().normalized;

        Vector3 halfExtents = new Vector3(width * 0.8f, height *1.6f, range*0.25f);
        Vector3 center = origin + direction * (range*0.66f);

        return new Bounds(center, halfExtents * 2f); 
    }
}
