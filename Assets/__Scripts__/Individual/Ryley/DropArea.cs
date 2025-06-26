using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour
{
    
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
        
        if (selected != null) 
        {
            Vector3 spawnPos = transform.position;
            spawnPos.z = 0;
            
            HandManager.Instance.PlayCardToWorld(selected, spawnPos);
            SelectCardManager.Instance.ClearSelection();
        }
        
        Debug.Log("Place Works");
    }
}