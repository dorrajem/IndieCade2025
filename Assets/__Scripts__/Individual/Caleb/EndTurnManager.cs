using UnityEngine;

public class EndTurnManager : MonoBehaviour
{
    public bool IsPlayerTurn = true;
    
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
