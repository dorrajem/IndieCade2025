using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    public CardData cardData;

    public void Setup(CardData data)
    {
        cardData = data;
        cardImage.sprite = cardData.Artwork;
    }

    public void Init(CardData newData, CardState newState)
    {
        cardData = newData;
        cardData.cardState = newState;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // UI, Artwork, Name, Cost
    }

    public CardData GetCardData() => cardData;

    public void PlayToBoard(Vector3 pos)
    {
        HandManager.Instance.PlayCardToWorld(this, pos);
    }

}