using UnityEngine;

public class ResourceManagement : MonoBehaviour
{
    // Current placeholder logic for sapling management
    const int saplingMax = 5;
    public bool turnStart = false;
    private int saplings = 0;

    // Current placeholder for sacrifices 
    //public int currentCards = 0;
    //private int cardMax = 4;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckCost(CardData data)
    {
        if (data.SaplingCostPoint <= saplings)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckEnums(CardData data)
    {
        if (data.cardState == CardState.OnTable && data.cardCategory == CardCategory.Nature)
        {
            return true;
        }
        return false;
    }

    public void SpendPoints(int amount)
    {
        if (saplings - amount >= 0)
        {
            saplings -= amount;
        }
        else
        {
            Debug.Log("Not enough Sapling Points, this shouldn't have been called");
        }
    }

    public void AddPoints(int amount)
    {
        saplings += amount;
        if (saplings > saplingMax)
        {
            saplings = saplingMax;
        }
    }
}
