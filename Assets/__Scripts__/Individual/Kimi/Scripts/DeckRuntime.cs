using System.Collections.Generic;
using UnityEngine;

public class DeckRuntime
{
    private List<CardData> _runtimeDeck;

    public void LoadFromTemplate(DeckData template)
    {
        _runtimeDeck = new List<CardData>(template.cards);
    }

    public void AddCard(CardData card) => _runtimeDeck.Add(card);

    public void Shuffle()
    {
        for (int i = 0; i < _runtimeDeck.Count; i++)
        {
            int j = Random.Range(i, _runtimeDeck.Count);
            (_runtimeDeck[i], _runtimeDeck[j]) = (_runtimeDeck[j], _runtimeDeck[i]);
        }
    }

    public CardData Draw()
    {
        if (_runtimeDeck.Count == 0) return null;
        var card = _runtimeDeck[0];
        _runtimeDeck.RemoveAt(0);
        return card;
    }
}
