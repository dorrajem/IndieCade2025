using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAttack", menuName = "Attack/Special")]
public class SpecialAttack : ScriptableObject, IAttackBehavior
{
    // Configs

    public void ExecuteAttack(GameObject attacker)
    {
        // special attack logic here
    }
}
