public static class CardDeathNotifier
{
    public static event System.Action<Card> OnCardDied;

    public static void NotifyCardDeath(Card card)
    {
        OnCardDied?.Invoke(card);
    }
}