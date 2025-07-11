using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack", menuName = "Attack/Basic")]
public class BasicAttack : ScriptableObject, IAttackBehavior
{
    [Header("Attack Settings")]
    public float range = 5f;
    public float width = 1f; // Width 
    public float height = 1f; // Height 
    public int damage = 10;
    public LayerMask targetLayer;

    public void ExecuteAttack(CardCombat attacker)
    {
        Vector3 origin = attacker.transform.position;
        Vector3 direction = attacker.GetAttackDirection();
        
        Vector3 halfExtents = new Vector3(width * 0.6f, height * 0.3f, range*0.1f);
        Vector3 center = origin + direction * (range * 0.35f);
        
        // Detect cards in the attack box
        Collider[] hits = Physics.OverlapBox(
            center, 
            halfExtents, 
            Quaternion.LookRotation(direction), 
            targetLayer
        );
        
        // Damage all targets in th box
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out CardCombat targetCard))
            {
                targetCard.TakeDamage(damage);
                Debug.Log($"{attacker.name} hit {targetCard.name} for {damage} damage!");
            }
        }
    }
    
    public Bounds GetAttackBoxBounds(CardCombat attacker)
    {
        Vector3 origin = attacker.transform.position;
        Vector3 direction = attacker.GetAttackDirection().normalized;

        Vector3 halfExtents = new Vector3(width * 0.6f, height * 0.3f, range*0.1f);
        Vector3 center = origin + direction * (range*0.35f);

        return new Bounds(center, halfExtents * 2f); 
    }
    
}