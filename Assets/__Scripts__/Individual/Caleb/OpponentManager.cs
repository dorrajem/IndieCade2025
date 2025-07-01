using UnityEngine;

public class OpponentManager : MonoBehaviour
{
    public ResourceManagement resourceManagement;
    public int opponentHealth;
    const int opponentMaxSaplings = 5;
    public int opponentSaplings = 1;
    public bool isOpponentTurn = false;
    EnemyAIController enemyAIController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void EnemyTurn(TurnManager turnManager)
    {
        isOpponentTurn = true;
       // enemyAIController.EnemyTakeTurn();

        isOpponentTurn = false;

        turnManager.TurnStart();
    }

}
