using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//battlecontroller, testing of turns and what works, does not currently function ingame
public class BattleControl : MonoBehaviour
{
    public static BattleControl instance;

    private void Awake()
    {
        instance = this;
    }

    public enum TurnOrder { playerActive, playerCardAttacks, enemyActive, enemyCardAttacks }
    public TurnOrder currentPhase;
    public int playerHealth, enemyHealth;

    public bool battleEnded;
    public float playerFirstChance = .5f;

    //public void PlayerAttack()
    //{
    //   StartCoroutine(PlayerAttackCo());
    //}

    //IEnumerator PlayerAttackCo()
    //{

    //}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AdvanceTurn();
        }
    }

    void Start()
    {
        if (Random.value > playerFirstChance)
        {
            currentPhase = TurnOrder.playerCardAttacks;
            AdvanceTurn();
        }

    }
    public void AdvanceTurn()
    {
        if (battleEnded == false)
        {
            currentPhase++;

            if ((int)currentPhase >= System.Enum.GetValues(typeof(TurnOrder)).Length)
            {
                currentPhase = 0;
            }

            //switch (currentPhase)

            //{
            //    case TurnOrder.playerActive:

            //        UIController.instance.endTurnButton.SetActive(true);

            //        break;

            //    case TurnOrder.playerCardAttacks:

            //       PlayerAttack();

            //    case TurnOrder.enemyActive:






            //}
        }
    }

    public void EndPlayerTurn()
    {
        UIController.instance.endTurnButton.SetActive(false);

        AdvanceTurn();
    }
    public void DamageEnemy(int damageAmount)
    {
        if (enemyHealth > 0 || battleEnded == false)
        {
            enemyHealth -= damageAmount;

            if (enemyHealth <= 0)
            {
                enemyHealth = 0;
                EndBattle();
            }
            UIController.instance.SetEnemyHealthText(enemyHealth);
            UIDamageIndicator damageClone = Instantiate(UIController.instance.enemyDamage, UIController.instance.enemyDamage.transform.parent);
            damageClone.damageText.text = damageAmount.ToString();
            damageClone.gameObject.SetActive(true);

        }
    }
    void EndBattle()
    {

    }


}
