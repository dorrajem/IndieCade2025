public static class CardDeathNotifier
{
    public static event System.Action<CardData> OnCardDied;

    public static void NotifyCardDeath(CardData card)
    {
        OnCardDied?.Invoke(card);
    }
}