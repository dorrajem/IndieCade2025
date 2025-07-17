using UnityEngine;

public enum ChoiceEventType
{
    Any,
    NatureOnly,
    IndustryOnly
}

[CreateAssetMenu (menuName = "Event/CardChoiceEvent")]
public class CardChoiceEvent : ScriptableObject
{
    [Header("Event Type")]
    public EventType eventType;
    public const int NumberOfChoices = 3;
}
