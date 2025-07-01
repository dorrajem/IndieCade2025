using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Card Deck")]
public class AIDeckData : ScriptableObject
{
    public List<CardData> cards;
}
