using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour
{
    
    void Update() 
    {
        CardHover selectedCard = CardHover.currentlySelected;
        if (selectedCard != null)
        {
            if (Input.GetMouseButtonDown(0)) // Left mouse button
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {   
                    Debug.Log("spwn");
                    SpawnCard();
                }
            }
        }
    }

    public void SpawnCard()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.z = 0; 

        // Instantiate card
        GameObject newCard = Instantiate(CardHover.currentlySelected.gameObject, spawnPos, Quaternion.identity, transform);
        
        
        CardDisplay card = newCard.GetComponent<CardDisplay>();
        if (card != null)
        {
            card.Setup(card.cardData);
        }
    }
}