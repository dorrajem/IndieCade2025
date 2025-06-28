using UnityEngine;

public class EndTurnManager : MonoBehaviour
{
    public bool IsPlayerTurn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Ends the player's current turn
    /// </summary>
    public void EndTurn()
    {
        IsPlayerTurn = false;
    }
}
