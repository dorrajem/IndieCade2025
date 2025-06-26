using UnityEngine;

public class Card2D : MonoBehaviour, ICardVisual
{
    public SpriteRenderer Renderer;

    public void ApplyVisual(CardData data)
    {
        Renderer.sprite = data.Artwork;
    }
}
