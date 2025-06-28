using UnityEngine;

public class CostCheck : MonoBehaviour
{
    public ResourceManagement playerResources; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanPlayCard(Card card)
    {
        return playerResources.CheckCost(card.cardData);
    }

    public bool TryPlayCard(Card card)
    {
        if (CanPlayCard(card))
        {
            playerResources.SpendPoints(card.cardData.SaplingCostPoint);
            return true;
        }
        return false;
    }
}
