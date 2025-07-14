using UnityEngine;
using System.Collections.Generic;
using Image = UnityEngine.UI.Image;

public class ResourceManagement : MonoBehaviour
{
    // Current placeholder logic for sapling management
    const int saplingMax = 5;
    private int saplings = 1;
    
    public Image saplingImage;
    
    public List<Sprite> saplingSprites = new();
    // Current placeholder for sacrifices 
    //public int currentCards = 0;
    //private int cardMax = 4;
    

    // Update is called once per frame
    void UpdateText()
    {
        for (int i = 0; i < saplingSprites.Count; i++)
        {
            if (i == saplings)
            {
                saplingImage.sprite = saplingSprites[i];
            }
        }
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

    public bool CheckEnums(CardData data,Card card)
    {
        if (card.cardState == CardState.OnTable && data.cardCategory == CardCategory.Nature)
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
            UpdateText();
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
        UpdateText();
    }
}
