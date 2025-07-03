using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card", menuName = "CardType")]
public class CardData : ScriptableObject
{
    public string CardName;
    public Sprite Artwork;
    public int SaplingCostPoint;
    public int SacrificeCostPoint;
    public int DisasterCostPoint;
    
    public int SacrificePoint = 1;

    public CardState cardState;
    public CardCategory cardCategory;
    public CardOwner cardOwner;
    
    public GameObject worldPrefab;
    
    [Header("Combat")]
    public int AttackPower;
    public int HealthPoint;
    public bool hasSpecialAbility = false;
    public float detectionRange = 5f;
    public LayerMask cardLayer;

    public virtual void OnSpecialAttack(CardCombat self, TurnManager turnManager)
    {
        // special attack logic
    }
}

public enum CardState
{
    InCardPile,
    InHand,
    OnTable,
    Die
}

public enum CardCategory
{
    Nature,
    Industry,
    Disaster
}

public enum CardOwner
{
    Player,
    Enemy
}
