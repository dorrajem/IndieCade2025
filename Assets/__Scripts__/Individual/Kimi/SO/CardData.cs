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

    
    public CardCategory cardCategory;
    
    public GameObject worldPrefab;
    
    [Header("Combat")]
    public int AttackPower;
    public int HealthPoint;
    public bool hasSpecialAbility = false;
    public float detectionRange = 5f;
    public LayerMask cardLayer;
    
    public Ability ability;
    public CardData changed;
    
    public AnimationClip deathClip;
}



public enum CardCategory
{
    Nature,
    Industry,
    Disaster
}
public enum Ability{
    Livestock,
    Grow,
    Ram,
    Mass_Destruction,
    Weaken,
    Target,
    Killer,
    Immortal,
    Farm,
    Heal,
    None
}


