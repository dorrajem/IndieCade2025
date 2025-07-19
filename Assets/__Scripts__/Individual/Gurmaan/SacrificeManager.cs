using System.Collections.Generic;
using UnityEngine;

public class SacrificeManager : MonoBehaviour
{
    public ResourceManagement resourceManagement;
    
    public List<DropArea> playerTiles;

    public bool Sacrificing = false;
    private float sacrificeCost;
    public List<GameObject> sacrifices = new List<GameObject>();

    public bool CanPlace = false;
    
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindWithTag("PManager").GetComponent<AudioManager>();
    }
    void Update()
    {
        // Finds out whether we are about to sacrifice or not
        var selected = SelectCardManager.Instance.currentCard;
        if (selected != null)
        {
            if (selected.cardData.cardCategory == CardCategory.Industry)
            {
                Sacrificing = true;
                sacrificeCost = selected.cardData.SacrificeCostPoint;
            }
            else Sacrificing = false;
        }
        else Sacrificing = false;
        if (!Sacrificing ) sacrifices.Clear();

        
        //Makes the nature cards on the board shake when an industry is selected
        for (int i = 0; i < playerTiles.Count; i++)
        {
            if (playerTiles[i].placedCard != null)
            {
                CardData natureData = playerTiles[i].placedCard.GetComponent<Card>().cardData;
                if (natureData.cardCategory == CardCategory.Nature)
                {
                    Shake(playerTiles[i].placedCard);
                }
            }
        }
        
        //Checks sacrifice cost
        if (Sacrificing)
        {
            if (sacrifices.Count > 0)
            {
                if (!CanPlace)
                {
                    for (int i = 0; i < sacrifices.Count; i++)
                    {
                        CardData sacrificeData = sacrifices[i].GetComponent<Card>().cardData;
                        sacrificeCost -= sacrificeData.SacrificePoint;
                        if (sacrificeCost <= 0)
                        {
                            CanPlace = true;
                        }
                    }
                }
                else
                {
                    audioManager.PlayCardSacrifice();
                    foreach (GameObject sacrifice in sacrifices)
                    {
                        CardCombat cardcom = sacrifice.GetComponent<CardCombat>();
                        StartCoroutine(cardcom.Die());
                    }
                    sacrifices.Clear();
                    Sacrificing = false;
                }
            }
        }
    }

    void Shake(GameObject card)
    {
        if (Sacrificing && !CanPlace)
        {
            float angle = Mathf.Sin(Time.time * 20) * 6;
            card.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            foreach (GameObject sacrifice in sacrifices)
            {
                SpriteRenderer spriteRenderer = sacrifice.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(1,0.5f,0.5f);
            }
        }
        else
        {
            card.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            SpriteRenderer spriteRenderer = card.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1,1,1);
        }
    }
}
