using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/AI Profile")]
public class EnemyAI : ScriptableObject
{
    public virtual (int, int) ExecuteTurn(
        List<CardData> hand,
        System.Action<CardData> playCardCallback,
        int currentSaplingPoints,
        int currentDisasterPoints,
        int boardSlotsAvailable)
    {
        List<CardData> playableNature = hand
            .Where(c => c.cardCategory == CardCategory.Nature && c.SaplingCostPoint <= currentSaplingPoints)
            .ToList();

        List<CardData> playableDisaster = hand
            .Where(c => c.cardCategory == CardCategory.Disaster && c.DisasterCostPoint <= currentDisasterPoints)
            .ToList();

        int boardSlotsRemaining = boardSlotsAvailable;

        //Debug.Log($"Enemy starting turn with {currentSaplingPoints} saplings and {currentDisasterPoints} disasterpoints and {boardSlotsAvailable} board slots.");

        while (playableDisaster.Count > 0 && boardSlotsRemaining > 0)
        {
            CardData bestCard = ChooseBestCard(playableDisaster);
            if (bestCard == null || bestCard.DisasterCostPoint > currentDisasterPoints)
                break;
            playCardCallback?.Invoke(bestCard);
            currentDisasterPoints -= bestCard.DisasterCostPoint;
            boardSlotsRemaining--;
            playableDisaster = hand
                .Where(c => c.cardCategory == CardCategory.Disaster && c.DisasterCostPoint <= currentDisasterPoints)
                .ToList();
        }

        
        while (playableNature.Count > 0 && boardSlotsRemaining > 0)
        {
            CardData bestCard = ChooseBestCard(playableNature);

            if (bestCard == null || bestCard.SaplingCostPoint > currentSaplingPoints)
                break;

            playCardCallback?.Invoke(bestCard);
            currentSaplingPoints -= bestCard.SaplingCostPoint;
            boardSlotsRemaining--;

            playableNature = hand
                .Where(c => c.cardCategory == CardCategory.Nature && c.SaplingCostPoint <= currentSaplingPoints)
                .ToList();
        }

        return (currentSaplingPoints, currentDisasterPoints);
    }

    protected virtual CardData ChooseBestCard(List<CardData> cards)
    {
        return cards.OrderByDescending(c => c.AttackPower).FirstOrDefault();
    }
}