using UnityEngine;

[CreateAssetMenu (fileName = "BasicAttack", menuName = "Attack/Basic")]
public class BasicAttack : ScriptableObject, IAttackBehavior
{
    public float range = 5f;
    public int damage = 10;
    public LayerMask targetLayer;

    public void ExecuteAttack(GameObject attacker)
    {
        Vector3 origin = attacker.transform.position;
        Vector3 direction = attacker.transform.right; // forward

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, range, targetLayer);
        if (hit.collider != null)
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
