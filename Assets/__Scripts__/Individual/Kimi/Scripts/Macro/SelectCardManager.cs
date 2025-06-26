using System;
using UnityEngine;

public class SelectCardManager : MonoBehaviour
{
    public static SelectCardManager Instance;
    public CardDisplay currentCard { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void SelectCard(CardDisplay card)
    {
        currentCard = card;
    }

    public void ClearSelection()
    {
        currentCard = null;
    }
}
