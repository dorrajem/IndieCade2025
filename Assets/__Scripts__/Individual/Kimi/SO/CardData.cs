using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card", menuName = "CardType")]
public class CardData : ScriptableObject
{
    public string CardName;
    public Sprite Artwork;
    public int SaplingCostPoint;
    public int AttackPower;
    public int HealthPoint;
    public const int SacrificePoint = 1;

    public CardState cardState;
    public CardCategory cardCategory;
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