using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Meant to control health bars and possibly other items if needed
public class UIController : MonoBehaviour
{
    public static UIController instance;
    public TMP_Text playerHealthText, enemyHealthText;
    public UIDamageIndicator playerDamage, enemyDamage;
    public GameObject endTurnButton;
    private void Awake()
    {
        instance = this;
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetPlayerHealthText(int healthAmount)
    {
        playerHealthText.text = "Player Health: " + healthAmount;
    }

    public void SetEnemyHealthText(int healthAmount)
    {
        enemyHealthText.text = "Enemy Health: " + healthAmount;
    }

}
