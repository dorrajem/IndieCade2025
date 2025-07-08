using UnityEngine;

public class AttackHolder : MonoBehaviour
{
    public ScriptableObject attackBehaviorSO;

    public IAttackBehavior GetBehavior()
    {
        return attackBehaviorSO as IAttackBehavior;
    }
}
