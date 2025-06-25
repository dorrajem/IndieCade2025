using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/AI Profile")]
public class EnemyAI : ScriptableObject
{
    public virtual int ExecuteTurn(
        List<CardData> hand,
        System.Action<CardData> playCardCallback,
        int currentSaplingPoints,
        int boardSlotsAvailable)
    {
        List<CardData> playable = hand
            .Where(c => c.SaplingCostPoint <= currentSaplingPoints)
            .ToList();

        int boardSlotsRemaining = boardSlotsAvailable;

        while (playable.Count > 0 && boardSlotsRemaining > 0)
        {
            CardData bestCard = ChooseBestCard(playable);

            if (bestCard == null || bestCard.SaplingCostPoint > currentSaplingPoints)
                break;

            playCardCallback?.Invoke(bestCard);
            currentSaplingPoints -= bestCard.SaplingCostPoint;
            boardSlotsRemaining--;

            playable = hand
                .Where(c => c.SaplingCostPoint <= currentSaplingPoints)
                .ToList();
        }

        return currentSaplingPoints;
    }

    protected virtual CardData ChooseBestCard(List<CardData> cards)
    {
        return cards.OrderByDescending(c => c.AttackPower).FirstOrDefault();
    }
}