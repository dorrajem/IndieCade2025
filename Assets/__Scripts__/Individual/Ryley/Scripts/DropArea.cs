using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour
{
    public bool isOccupied = false;
    public ResourceManagement resourceManagement;
    public SacrificeManager sacrificeManager;

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
                card.cardOwner = CardOwner.Player;
            }
        }
    }

    private void OnMouseDown()
    {
        var selected = SelectCardManager.Instance.currentCard;
        
        
        if (selected != null && !isOccupied)
        {
            if (selected.cardData.cardCategory == CardCategory.Nature)
            {
                if(!resourceManagement.CheckCost(selected.cardData))
                {
                    return;
                }
                resourceManagement.SpendPoints(selected.cardData.SaplingCostPoint);
            }

            if (selected.cardData.cardCategory == CardCategory.Industry)
            {
                if (!sacrificeManager.CanPlace) return;
                else sacrificeManager.CanPlace = false;
            }
            

            Vector3 spawnPos = transform.position;
            spawnPos.z = 0;
            
            GameObject newCard = HandManager.Instance.PlayCardToWorld(selected, spawnPos, CardOwner.Player, Index);

            isOccupied = true;
            placedCard = newCard;

            var slotRef = newCard.AddComponent<SlotReference>();
            slotRef.slot = this;
            
            SelectCardManager.Instance.ClearSelection();
        }
        
    }

    public void Change(CardData changed, SlotReference slotReference, CardOwner owner)
    {
        Vector3 spawnPos = slotReference.slot.transform.position;
        spawnPos.z = 0;
            
        GameObject newCard = HandManager.Instance.ChangeCardToWorld(changed, spawnPos, owner, slotReference.slot.Index);

        slotReference.slot.isOccupied = true;
        slotReference.slot.placedCard = newCard;

        var slotRef = newCard.AddComponent<SlotReference>();
        slotRef.slot = slotReference.slot;
    }

    public void ClearSlot()
    {
        isOccupied = false;
        placedCard = null;
    }
}