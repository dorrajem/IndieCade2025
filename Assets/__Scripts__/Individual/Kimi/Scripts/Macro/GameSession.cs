using System;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    public DeckRuntime PlayerDeck { get; private set; } = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
