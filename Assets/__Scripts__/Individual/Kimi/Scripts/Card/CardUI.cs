using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, ICardVisual
{
    private Image artworkImage;

    public void ApplyVisual(CardData data)
    {
        artworkImage.sprite = data.Artwork;
    }
}
