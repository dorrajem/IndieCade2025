using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public EnemyAI enemyAI;
    public DeckData enemyDeckTemplate; 
    private DeckRuntime enemyCardDeck;

    private List<CardData> enemyHand = new();
    public int maxSaplingPoints = 5;
    private int currentSaplingPoints;
    
    public int currentDisasterPoints;
    public int maxDisasterPoints = 7;

    public int maxBoardSlots = 4;
    public int initialDrawCount = 6;

    private List<AIDropArea> enemyDropAreas = new();
    private List<DropArea> playerDropAreas = new();
    private Dictionary<int, AIDropArea> enemySlotMap;
    private Dictionary<int, DropArea> playerSlotMap;
    public bool canPlayDisaster = false;

    public int natureDeaths = 0;
    public int maxNatureDeaths = 4;
    
    private TurnManager turnManager;

    private void Awake()
    {
        enemyDropAreas = new List<AIDropArea>(FindObjectsByType<AIDropArea>(FindObjectsSortMode.None));
        turnManager = GameObject.FindWithTag("Manager").GetComponent<TurnManager>();

       }

    private void Start()
    {
        currentSaplingPoints = 1;
        currentDisasterPoints = 0;

        enemyCardDeck = new DeckRuntime();
        enemyCardDeck.LoadFromTemplate(enemyDeckTemplate);
        enemyCardDeck.Shuffle();

        DrawNewCard(initialDrawCount);
        InitializeSlotMaps(); 
        EnemyTakeTurn(turnManager);
    }

    private void OnEnable()
    {
        CardDeathNotifier.OnCardDied += HandleCardDeath;
    }

    private void OnDisable()
    {
        CardDeathNotifier.OnCardDied -= HandleCardDeath;
    }

    private void HandleCardDeath(CardData cardData)
    {
        if (cardData.cardCategory == CardCategory.Nature)
        {
            natureDeaths++;
            SetDisasterPoints(1); 
        }
    }

    public void EnemyTakeTurn(TurnManager turnManager)
    {
        currentSaplingPoints += 2;
        currentSaplingPoints = Mathf.Min(currentSaplingPoints, maxSaplingPoints);
        SetDisasterPoints(2);

        DrawNewCard(1);

        int boardSpace = GetNumAvailableSlots();

        (currentSaplingPoints, currentDisasterPoints) = enemyAI.ExecuteTurn(
            enemyHand,
            cardData =>
            {
                var slot = GetBestCounterSlot(cardData);
                if (slot != null)
                {
                    GameObject cardGO = new GameObject("EnemyCard");
                    Card card = cardGO.AddComponent<Card>();
                    card.Init(cardData, false);
                    slot.PlaceEnemyCard(card);
                    enemyHand.Remove(cardData);
                }
            },
            currentSaplingPoints,
            currentDisasterPoints,
            boardSpace
        );
        turnManager.TurnStart();
    }

    private void DrawNewCard(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CardData card = enemyCardDeck.Draw();
            if (card != null)
            {
                enemyHand.Add(card);
            }
        }
    }

    private AIDropArea GetNextAvailableDropArea()
    {
        return enemyDropAreas.Find(area => !area.isOccupied);
    }

    public int GetNumAvailableSlots()
    {
        int val = 0;
        foreach (var area in enemyDropAreas)
        {
            if (!area.isOccupied)
                val++;
        }
        return val;
    }

    public bool CheckCanPlayDisaster()
    {
        if (natureDeaths >= maxNatureDeaths)
        {
            canPlayDisaster = true;
            return true;
        }
        if (canPlayDisaster)
            return true;
       playerDropAreas = new List<DropArea>(FindObjectsByType<DropArea>(FindObjectsSortMode.None));
       foreach (var area in playerDropAreas)
       {
            if (area.isOccupied && area.placedCard != null)
            {
                Card card = area.placedCard.GetComponent<Card>();
                if (card != null && card.cardData.cardCategory == CardCategory.Industry)
                {
                    //Debug.Log("Industry card found on player board");
                    canPlayDisaster = true;
                    return true;
                }
            }
       }
        return false; 
    }

    public void SetDisasterPoints(int increment)
    {
        if (!CheckCanPlayDisaster())
        {
            return;
        }
        currentDisasterPoints += increment;
        currentDisasterPoints = Mathf.Min(currentDisasterPoints, maxDisasterPoints);
    }

    private void InitializeSlotMaps()
    {
        enemySlotMap = enemyDropAreas
            .ToDictionary(
                slot => ParseSlotIndex(slot.gameObject.name),
                slot => slot
            );
        var playerSlots = new List<DropArea>(FindObjectsByType<DropArea>(FindObjectsSortMode.None));
        playerSlotMap = playerSlots
            .ToDictionary(
                slot => ParseSlotIndex(slot.gameObject.name),
                slot => slot
            );
    }

    private int ParseSlotIndex(string name)
    {
        // Assumes names like "Slot_1" through "Slot_8"
        if (name.StartsWith("Slot_") && int.TryParse(name.Substring(5), out int idx))
            return idx;
        Debug.LogError("Unrecognized slot name: " + name);
        return -1;
    }

    private AIDropArea GetBestCounterSlot(CardData cardToPlay)
    {
        InitializeSlotMaps();

        foreach (var kvp in playerSlotMap)
        {
            int playerIndex = kvp.Key;
            var playerArea = kvp.Value;

            if (playerArea.isOccupied && enemySlotMap.TryGetValue(playerIndex + 4, out var enemyArea))
            {
                if (!enemyArea.isOccupied)
                    return enemyArea;  
            }
        }
        return enemySlotMap.Values.FirstOrDefault(area => !area.isOccupied);
    }


}