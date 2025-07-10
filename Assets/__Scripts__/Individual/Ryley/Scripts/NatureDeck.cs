using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NaturePack", menuName = "Nature Deck")]
public class NatureDeck : ScriptableObject
{
    public List<CardData> cards;
}
