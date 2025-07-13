using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCombat : MonoBehaviour, IDamageable
{
    public Card _card;

    [Header("Runtime")] 
    public int currentHP { get; private set; }
    public int attack { get; private set; }

    private IAttackBehavior _attackBehavior;

    public float detectionRange = 5f;
    public LayerMask cardLayer;
    
    private AudioManager audioManager;
    private TurnManager turnManager;
    private Card2D card2d;
    
    public List<ScriptableObject> attackBehaviorSO = new();

    private void Awake()
    {
        turnManager = GameObject.FindWithTag("Manager").GetComponent<TurnManager>();
        audioManager = GameObject.FindWithTag("Manager").GetComponent<AudioManager>();
        _card = GetComponent<Card>();
        card2d = GetComponent<Card2D>();
    }
    public IAttackBehavior GetBehavior()
    {
        int dmg = _card.GetCardData().AttackPower;
        Debug.Log(_card.cardData.CardName+":"+dmg);
        if (dmg==0)
        {
            return null;
        }
        return attackBehaviorSO[dmg-1] as IAttackBehavior;
    }

    private void Start()
    {
        var data = _card.GetCardData();
        currentHP = data.HealthPoint;
        attack = data.AttackPower;
        _attackBehavior = GetBehavior();
    }

    public void TryAttack()
    {
        if ((turnManager.gameTurn!=GameTurn.PlayerCard && turnManager.gameTurn!=GameTurn.EnemyCard) || _card.GetCardData().cardState != CardState.OnTable || _card.GetCardData().cardState == CardState.Die || _card.GetCardData().AttackPower==0)
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
            StartCoroutine(AttackMove());
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
        if (currentHP <= 0 && _card.GetCardData().cardState != CardState.Die)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        _card.GetCardData().cardState = CardState.Die;
        audioManager.PlayCardDie();
        _card.PlayDeathAnim();

        yield return new WaitForSeconds(1f); 

        card2d.DestroySelf();
    }

#if UNITY_EDITOR
private void OnDrawGizmosSelected()
{
    if (_attackBehavior is BasicAttack basicAttack)
    {
        Bounds box = basicAttack.GetAttackBoxBounds(this);

        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(box.center, Quaternion.LookRotation(GetAttackDirection()), Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, box.size);
    }
}
#endif


    public Vector3 GetAttackDirection()
    {
        return _card.cardOwner == CardOwner.Player 
            ? transform.up 
            : -transform.up;
    }
}
