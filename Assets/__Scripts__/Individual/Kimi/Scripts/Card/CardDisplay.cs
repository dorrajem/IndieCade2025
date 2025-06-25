using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]private Image cardImage;
    [SerializeField]private TMP_Text cardName;
    [SerializeField]private TMP_Text cost;
    [SerializeField]private TMP_Text attackPower;
    [SerializeField]private TMP_Text healthPoint;

    public CardData cardData;
    
    public void Setup(CardData data)
    {
        cardData = data;
        cardImage.sprite = data.Artwork;
        cardName.text = data.name;
        cost.text = data.SaplingCostPoint.ToString();
        attackPower.text = data.AttackPower.ToString();
        healthPoint.text = data.HealthPoint.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       // PlayCard();
    }

    private void PlayCard()
    {
        HandManager.Instance.RemoveCardFromHand(this.gameObject);
    }
}
