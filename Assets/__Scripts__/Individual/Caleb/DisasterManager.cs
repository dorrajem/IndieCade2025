using UnityEngine;
using System.Collections.Generic;
using System;

public class DisasterManager : MonoBehaviour
{
    public List<DropArea> playerSlots;
    public TurnManager manager;
    //private bool hasDisasterHappened = false;
    public int disasterCount = 2;
    private int callCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.turnCount == 5 && callCount == 0)
        {
            //Debug.Log("A disaster can happen at any moment now");
            callCount++;
        }

        if (manager.turnCount > 5 && disasterCount == 0)
        {
            //Disaster();
            disasterCount = 2;
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
            Debug.Log(playerSlots[randomIndex]);
        }
        Debug.Log("Disaster Happened!");
        //hasDisasterHappened = true;
    }
}
