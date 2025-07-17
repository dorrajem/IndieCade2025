using System;
using UnityEngine;
using System.Collections.Generic;

public class UICardChoice : MonoBehaviour
{
    public Transform cardParent;
    public GameObject cardDisplayPrefab;

    public void ShowChoices(List<CardData> options, Action<CardData> onChosen)
    {
        foreach (Transform child in cardParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var card in options)
        {
            var go = Instantiate(cardDisplayPrefab, cardParent);
            var display = go.GetComponent<UICardDisplay>();
            display.SetUp(card, onChosen);
        }
    }
}
