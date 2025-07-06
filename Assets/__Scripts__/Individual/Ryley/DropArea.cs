using System;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour
{
    public bool isOccupied = false;
    public ResourceManagement resourceManagement;
    public SacrificeManager sacrificeManager;

    public GameObject placedCard;
    // private void Update() 
    // {
    //     CardHover selectedCard = CardHover.currentlySelected;
    //     if (selectedCard != null)
    //     {
    //         if (Input.GetMouseButtonDown(0)) // Left mouse button
    //         {
    //             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //             RaycastHit hit;
    //             if (Physics.Raycast(ray, out hit))
    //             {
    //                 if (hit.collider != null && hit.collider.gameObject == gameObject)
    //                 {
    //                     SpawnCard();
    //                 }
    //             }
    //
    //         }
    //     }
    // }
    //
    //
    //
    // public void SpawnCard()
    // {
    //     Vector3 spawnPos = transform.position;
    //     spawnPos.z = 0; 
    //
    //     // Instantiate card
    //     GameObject newCard = Instantiate(CardHover.currentlySelected.gameObject, spawnPos, Quaternion.identity, transform);
    //     
    //     
    //     CardDisplay card = newCard.GetComponent<CardDisplay>();
    //     if (card != null)
    //     {
    //         card.Setup(card.cardData);
    //     }
    // }
    

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
            
            GameObject newCard = HandManager.Instance.PlayCardToWorld(selected, spawnPos);

            isOccupied = true;
            placedCard = newCard;

            var slotRef = newCard.AddComponent<SlotReference>();
            slotRef.slot = this;
            
            SelectCardManager.Instance.ClearSelection();
            
            CameraController.Instance.BackCam();
        }
        
    }

    public void ClearSlot()
    {
        isOccupied = false;
        placedCard = null;
    }
}