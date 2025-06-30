using UnityEngine;

public class OpponentManager : MonoBehaviour
{
    public ResourceManagement resourceManagement;
    public int opponentHealth;
    const int opponentMaxSaplings = 5;
    public int opponentSaplings = 1;
    public bool isOpponentTurn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (opponentHealth <= 0)
        {
            Debug.Log("You Win!!!");
        }
    }
}
