using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "BasicAttack", menuName = "Attack/Basic")]
public class BasicAttack : ScriptableObject, IAttackBehavior
{
    public float range = 5f;
    public float arcHeight = 2f;
    public float hitRadius = 0.3f;
    public int damage = 10;
    public LayerMask targetLayer;
    // Parabola
    

    public void ExecuteAttack(CardCombat attacker)
    {
        Vector3 start = attacker.transform.position;
        Vector3 dir = attacker.GetAttackDirection();
        Vector3 end = start + attacker.transform.right * range;

        var target = TryParabolaDetect(start, end, arcHeight, 20,  0.3f, targetLayer);
        
        if (target != null)
        {
            target.TakeDamage(attacker.attack); // TODO: take actual damage from the card
            Debug.LogWarning("Attacked enemy card");
        }
        else
        {
            Debug.LogWarning("Attacked enemy player");
            // no card in front, attack enemy player instead
        }
    }

    private CardCombat TryParabolaDetect(Vector3 start, Vector3 end, float height, int steps, float radius, LayerMask layer)
    {
        for (int i = 0; i < steps; i++)
        {
            float t = i / (float)steps;
            Vector3 point = CalculateParabola(start, end, height, t);
            Collider[] hits = Physics.OverlapSphere(point, radius, layer);
            if (hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.TryGetComponent(out CardCombat cc))
                    {
                        return cc;
                    }
                }
            }
        }

        return null;
    }

    public List<Vector3> GetParabolaPoints(Vector3 start, Vector3 end, float arcHeight, int steps)
    {
        List<Vector3> points = new();
        for (int i = 0; i < steps; i++)
        {
            float p = i / (float)steps;
            points.Add(CalculateParabola(start, end, arcHeight, p));
        }

        return points;
    }
    
    private Vector3 CalculateParabola(Vector3 start, Vector3 end, float height, float t)
    {
        Vector3 mid = (start + end) / 2 + Vector3.up * height;
        Vector3 m1 = Vector3.Lerp(start, mid, t);
        Vector3 m2 = Vector3.Lerp(mid, end, t);
        return Vector3.Lerp(m1, m2, t);
    }
    
    
}
