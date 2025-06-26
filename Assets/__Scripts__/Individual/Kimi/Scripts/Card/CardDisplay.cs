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
}