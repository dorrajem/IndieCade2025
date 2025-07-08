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
    
    
}
