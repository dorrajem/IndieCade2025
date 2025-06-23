using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "CardType")]
public class SO_Card : ScriptableObject
{
    public int saplingPoint;
    private const int sacrifiesPoint = 1;
}

public enum CardState
{
    InCardPile,
    InHand,
    OnTable,
    Die
}