using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IndustryPack", menuName = "Industry Deck")]
public class IndustryDeck : ScriptableObject
{
    public List<CardData> cards;
}
