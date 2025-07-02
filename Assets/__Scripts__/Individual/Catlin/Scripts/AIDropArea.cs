using UnityEngine;

public class AIDropArea : MonoBehaviour
{
    public bool isOccupied = false;
    public ResourceManagement resourceManagement;

    public GameObject placedCard;

    public void PlaceEnemyCard(Card card)
    {
        if (card != null && !isOccupied)
        {
            if (card.cardData.cardCategory == CardCategory.Nature)
            {
                if (!resourceManagement.CheckCost(card.cardData))
                {
                    Debug.Log("Not enough resources to play this card.");
                    return;
                }
                resourceManagement.SpendPoints(card.cardData.SaplingCostPoint);
            }

            

            Vector3 spawnPos = transform.position;
            spawnPos.z = 0;

            GameObject newCard = HandManager.Instance.PlayCardToWorld(card, spawnPos);

            isOccupied = true;
            placedCard = newCard;

            var slotRef = newCard.AddComponent<AISlotReference>();
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