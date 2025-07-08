using System;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private ScriptableObject attackBehaviorSO;
    private IAttackBehavior _attackBehavior;

    private void Awake()
    {
        _attackBehavior = attackBehaviorSO as IAttackBehavior;
    }

    public void PerformAttack()
    {
        if (_attackBehavior != null)
        {
            _attackBehavior.ExecuteAttack(gameObject);
        }
    }
}
