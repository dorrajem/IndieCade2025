using UnityEngine;

public class ResourceManagement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Current placeholder logic for sapling management
    const saplingMax = 5;
    public bool turnStart = false;
    private int saplings = 0;

    // Current placeholder for sacrifices 
    public int currentCards = 0;
    private int cardMax = 4;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnStart)
        {
            saplings += 2;
            if (saplings > saplingMax)
            {
                saplings = saplingMax;
            }
            turnStart = false;
        }
    }
}
