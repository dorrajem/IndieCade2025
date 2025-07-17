using System.Collections.Generic;
using UnityEngine;

public class DeckRuntime
{
    private List<CardData> _runtimeDeck = new ();

    public void LoadFromTemplate(DeckData template)
    {
        _runtimeDeck = new List<CardData>(template.cards);
    }

    public bool IsEmpty()
    {
        return _runtimeDeck == null || _runtimeDeck.Count == 0;
    }

    public void AddCard(CardData card) => _runtimeDeck.Add(card);

    //Caleb's Edit
    public void RemoveCard(CardData card)
    {
        _runtimeDeck.Remove(card);
    }

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

    public CardData DrawInit()
    {
        var card = _runtimeDeck[0];
        for (int i = 0; i < _runtimeDeck.Count; i++)
        {
            card = _runtimeDeck[i];
            if (card.cardCategory == CardCategory.Nature && card.SaplingCostPoint<=2)
            {
                _runtimeDeck.RemoveAt(i);
                return card;
            }
        }
        return card;
    }

    // For debug
    public List<CardData> GetAllCards()
    {
        return _runtimeDeck;
    }
}
