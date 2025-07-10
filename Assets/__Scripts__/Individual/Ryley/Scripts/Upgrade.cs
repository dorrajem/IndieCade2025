using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public GameObject upgraded;
    private int newHealth;
    private int newAttk;
    public CardData cardData;


    void Update()
    {

    }
    public void OnTriggerEnter()
    {
        if (this.cardData != null)
        {
            upgraded = upgraded.GetComponent<DropArea>().placedCard;
            Debug.Log("this is " + upgraded);

            newHealth = cardData.HealthPoint += 2;
            newAttk = cardData.AttackPower += 1;
            
        }
        //
        //Debug.LogWarning(newHealth);
    }
}
