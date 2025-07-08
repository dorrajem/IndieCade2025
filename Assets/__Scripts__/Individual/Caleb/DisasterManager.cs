using UnityEngine;
using System.Collections.Generic;
using System;

public class DisasterManager : MonoBehaviour
{
    public List<DropArea> playerSlots;
    public TurnManager manager;
    private bool hasDisasterHappened = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.turnCount == 5)
        {
            Debug.Log("A disaster can happen at any moment now");
        }

        if (manager.turnCount > 5 && !hasDisasterHappened)
        {
            Disaster();
        }
        /*
        if (slot1.isOccupied && slot1.placedCard != null)
        {
            Destroy(slot1.placedCard);
            slot1.ClearSlot(); // resets the slot state
        }
        */
    }

    void Disaster()
    {
        int randomIndex = UnityEngine.Random.Range(0, playerSlots.Count);

        if (playerSlots[randomIndex].isOccupied && playerSlots[randomIndex].placedCard != null)
        {
            Destroy(playerSlots[randomIndex].placedCard);
            playerSlots[randomIndex].ClearSlot(); // resets the slot state
        }
        hasDisasterHappened = true;
    }
}
