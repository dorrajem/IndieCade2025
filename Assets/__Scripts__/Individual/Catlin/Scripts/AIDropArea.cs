using UnityEngine;

public class AIDropArea : MonoBehaviour
{
    public bool isOccupied = false;
    public ResourceManagement resourceManagement;

    public GameObject placedCard;
    
    private Card card;

    public int Index;

    void Update()
    {
        if (placedCard != null)
        {
            card = placedCard.GetComponent<Card>();
            if (card.cardData.cardCategory == CardCategory.Nature)
            {
                card.cardOwner = CardOwner.Enemy;
            }
        }
    }
    
    public void PlaceEnemyCard(Card card)
    {
        //Debug.Log($"Attempting to place {card.cardData.CardName} with cost {card.cardData.SaplingCostPoint} and disaster cost {card.cardData.DisasterCostPoint}");
        if (card != null && !isOccupied)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.z = 0;

            GameObject newCard = HandManager.Instance.PlayCardToWorld(card, spawnPos, CardOwner.Enemy, Index);
            Card cardNew = newCard.GetComponent<Card>();
            cardNew.cardOwner = CardOwner.Enemy;

            isOccupied = true;
            placedCard = newCard;

            var slotRef = newCard.AddComponent<AISlotReference>();
            slotRef.slot = this;

            SelectCardManager.Instance.ClearSelection();
        }
    }
    
    public void AIChange(CardData changed, AISlotReference slotReference, CardOwner owner)
    {
        Vector3 spawnPos = slotReference.slot.transform.position;
        spawnPos.z = 0;
            
        GameObject newCard = HandManager.Instance.ChangeCardToWorld(changed, spawnPos, owner, slotReference.slot.Index);

        slotReference.slot.isOccupied = true;
        slotReference.slot.placedCard = newCard;

        var slotRef = newCard.AddComponent<AISlotReference>();
        slotRef.slot = slotReference.slot;
    }

    public void ClearSlot()
    {
        isOccupied = false;
        placedCard = null;
    }
}